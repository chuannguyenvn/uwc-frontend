using System;
using System.Collections.Generic;
using System.Linq;
using Commons.Communications.Mcps;
using Commons.Endpoints;
using LocalizationNS;
using Requests;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UI.Reusables.Control.Sort;
using UI.Views.Mcps.AssignTaskProcedure;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Mcps
{
    public class McpsView : View
    {
        private readonly bool _isTaskAssigning;
        private readonly Dictionary<string, McpListEntry> _mcpListEntriesByAddress = new();

        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;
        private McpInformationPopup _mcpInformationPopup;

        public McpsView(bool isTaskAssigning = false) : base(nameof(McpsView))
        {
            _isTaskAssigning = isTaskAssigning;
            if (_isTaskAssigning) AddToClassList("task-assigning");

            ConfigureUss(nameof(McpsView));

            AddToClassList("side-view");

            CreateControls();
            CreateScrollView();

            if (_isTaskAssigning) ChooseMcpsStep.McpListChanged += SortByAssigningOrder;
            else CreateFullscreenPopup();

            DataStoreManager.Mcps.ListView.DataUpdated += DataUpdatedHandler;
        }

        ~McpsView()
        {
            DataStoreManager.Mcps.ListView.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateControls()
        {
            _listControl = new ListControl(SearchHandler);
            Add(_listControl);

            _listControl.CreateSortButton(Localization.GetSentence(Sentence.McpsView.FILL_LEVEL),
                () => DataUpdatedHandler(DataStoreManager.Mcps.ListView.Data));
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop) { name = "ScrollView" };
            Add(_scrollView);
        }

        private void CreateFullscreenPopup()
        {
            _mcpInformationPopup = new McpInformationPopup();
            Root.Instance.AddPopup(_mcpInformationPopup);
        }

        private void DataUpdatedHandler(GetMcpDataResponse data)
        {
            var mcpEntries = new List<McpListEntry>();

            _scrollView.Clear();
            foreach (var mcpData in data.Results)
            {
                var entry = new McpListEntry(mcpData, _isTaskAssigning);
                mcpEntries.Add(entry);
                _mcpListEntriesByAddress[mcpData.Address] = entry;

                if (!_isTaskAssigning)
                {
                    entry.Clicked += () =>
                    {
                        DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<GetSingleMcpDataResponse>(
                            Endpoints.McpData.GetSingle,
                            new GetSingleMcpDataRequest
                            {
                                McpId = mcpData.Id,
                                HistoryCountLimit = 100,
                                HistoryDateTimeLimit = DateTime.Now.AddDays(-1),
                            },
                            (success, result) =>
                            {
                                if (success)
                                {
                                    mcpData.McpEmptyRecords = result.Result.McpEmptyRecords;
                                    mcpData.McpFillLevelLogs = result.Result.McpFillLevelLogs;
                                    _mcpInformationPopup.SetContent(mcpData);
                                    _mcpInformationPopup.Show();
                                }
                            }));
                    };
                }
            }

            if (_isTaskAssigning)
            {
                SortByAssigningOrder();
                return;
            }

            if (_listControl.SortStates[0] == SortType.Ascending)
                mcpEntries.Sort((a, b) => a.CurrentLoadPercentage.CompareTo(b.CurrentLoadPercentage));
            else if (_listControl.SortStates[0] == SortType.Descending)
                mcpEntries.Sort((a, b) => b.CurrentLoadPercentage.CompareTo(a.CurrentLoadPercentage));

            foreach (var mcpEntry in mcpEntries) _scrollView.AddToScrollView(mcpEntry);
        }

        private void SortByAssigningOrder()
        {
            var mcpEntries = _mcpListEntriesByAddress.Values.ToList();

            _scrollView.Clear();
            for (var i = ChooseMcpsStep.ChosenMcpIds.Count - 1; i >= 0; i--)
            {
                var id = ChooseMcpsStep.ChosenMcpIds[i];
                var entry = mcpEntries.Find(e => e.McpData.Id == id);
                mcpEntries.Remove(entry);
                mcpEntries.Insert(0, entry);
                entry.RefreshAssigningStatus();
            }

            if (_listControl.SortStates[0] == SortType.Ascending)
                mcpEntries.Sort((a, b) => a.CurrentLoadPercentage.CompareTo(b.CurrentLoadPercentage));
            else if (_listControl.SortStates[0] == SortType.Descending)
                mcpEntries.Sort((a, b) => b.CurrentLoadPercentage.CompareTo(a.CurrentLoadPercentage));

            foreach (var mcpEntry in mcpEntries) _scrollView.AddToScrollView(mcpEntry);
        }

        public override void FocusView()
        {
            DataStoreManager.Mcps.ListView.Focus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.Mcps.ListView.Unfocus();
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var (address, entry) in _mcpListEntriesByAddress)
            {
                if (Utility.CreateSearchString(address).Contains(text) || text == "")
                {
                    entry.style.display = DisplayStyle.Flex;
                }
                else
                {
                    entry.style.display = DisplayStyle.None;
                }
            }
        }
    }
}