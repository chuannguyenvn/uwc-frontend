using System.Collections.Generic;
using System.Linq;
using Commons.Communications.UserProfiles;
using Requests;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UI.Reusables.Control.Sort;
using UI.Views.Mcps.AssignTaskProcedure;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Workers
{
    public class WorkersView : View
    {
        private readonly bool _isTaskAssigning;
        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;
        private WorkerInformationPopup _workerInformationPopup;

        private List<WorkerListEntry> _workerListEntries = new();

        public WorkersView(bool isTaskAssigning = false) : base(nameof(WorkersView))
        {
            _isTaskAssigning = isTaskAssigning;
            if (_isTaskAssigning) AddToClassList("task-assigning");

            ConfigureUss(nameof(WorkersView));

            AddToClassList("side-view");

            CreateControls();
            CreateScrollView();

            if (_isTaskAssigning) ChooseWorkerStep.WorkerIdChanged += SortByAssigningOrder;
            else CreateFullscreenPopup();

            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated += DataUpdatedHandler;
        }

        ~WorkersView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateControls()
        {
            _listControl = new ListControl(SearchHandler);
            Add(_listControl);
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop) { name = "ScrollView" };
            Add(_scrollView);
        }

        private void CreateFullscreenPopup()
        {
            _workerInformationPopup = new WorkerInformationPopup();
            Root.Instance.AddPopup(_workerInformationPopup);
        }

        public override void FocusView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.Focus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.UserProfile.AllWorkerProfileList.Unfocus();
        }

        private void DataUpdatedHandler(GetAllWorkerProfilesResponse response)
        {
            _scrollView.Clear();
            _workerListEntries.Clear();
            foreach (var userProfile in response.WorkerProfiles)
            {
                var entry = new WorkerListEntry(userProfile, _isTaskAssigning);
                _scrollView.AddToScrollView(entry);
                _workerListEntries.Add(entry);

                if (!_isTaskAssigning)
                {
                    entry.Clicked += () =>
                    {
                        _workerInformationPopup.SetContent(userProfile);
                        _workerInformationPopup.Show();
                    };
                }
            }

            if (_isTaskAssigning) SortByAssigningOrder();
        }

        private void SortByAssigningOrder()
        {
            var workerEntries = _workerListEntries.ToList();
            if (workerEntries.Count == 0) return;
            
            _scrollView.Clear();
            var id = ChooseWorkerStep.WorkerId;
            if (id != -1)
            {
                var entry = workerEntries.Find(e => e.Profile.Id == id);
                workerEntries.Remove(entry);
                workerEntries.Insert(0, entry);
                entry.RefreshAssigningStatus();
            }

            foreach (var mcpEntry in workerEntries) _scrollView.AddToScrollView(mcpEntry);
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var entry in _workerListEntries)
            {
                if (Utility.CreateSearchString(entry.Profile.FirstName, entry.Profile.LastName, entry.Profile.Address,
                        entry.Profile.Gender.ToString(), entry.Profile.UserRole.ToString(), entry.Profile.DateOfBirth.ToString()).Contains(text) ||
                    text == "")
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