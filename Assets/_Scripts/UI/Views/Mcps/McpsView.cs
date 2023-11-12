using System.Collections.Generic;
using Commons.Models;
using Requests;
using UI.Base;
using UI.Reusables;
using UI.Views.Mcps.AssignTaskProcedure;
using UnityEngine.UIElements;
using UnityEngine;

namespace UI.Views.Mcps
{
    public class McpsView : View
    {
        private VisualElement _controlsContainer;
        private SearchBar _searchBar;
        private AssignTaskFlow _assignTaskFlow;

        private McpList _mcpList;

        private VisualElement _assigningButton;
        private VisualElement _assigningButtonIcon;

        public McpsView() : base(nameof(McpsView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Mcps/McpsView"));
            AddToClassList("side-view");
            AddToClassList("mcps-view");

            CreateSearchBar();
            CreateMcpList();
            CreateAssigningButton();
        }

        private void CreateSearchBar()
        {
            _controlsContainer = new VisualElement { name = "ControlsContainer" };
            Add(_controlsContainer);

            _searchBar = new SearchBar(SearchHandler);
            _controlsContainer.Add(_searchBar);

            _assignTaskFlow = new AssignTaskFlow();
            _controlsContainer.Add(_assignTaskFlow);
        }

        private void CreateMcpList()
        {
            _mcpList = new McpList();
            Add(_mcpList);
        }

        private void CreateAssigningButton()
        {
            _assigningButton = new VisualElement { name = "AssigningButton" };
            _assigningButton.RegisterCallback<ClickEvent>(ev => ToggleAssigningMode(true));
            Add(_assigningButton);

            _assigningButtonIcon = new VisualElement { name = "AssigningButtonIcon" };
            _assigningButton.Add(_assigningButtonIcon);
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
            text = Utility.RemoveDiacritics(text).ToLower();
            foreach (var (address, entry) in _mcpList.McpListEntriesByAddress)
            {
                if (Utility.RemoveDiacritics(address).ToLower().Contains(text) || text == "")
                {
                    entry.style.display = DisplayStyle.Flex;
                }
                else
                {
                    entry.style.display = DisplayStyle.None;
                }
            }
        }

        public void ToggleAssigningMode(bool isAssigning)
        {
        }
    }
}