using System.Collections.Generic;
using Commons.Models;
using Requests;
using UI.Base;
using UI.Reusables;
using UnityEngine.UIElements;
using Utilities;
using Random = UnityEngine.Random;

namespace UI.Views.Mcps
{
    public class McpsView : View
    {
        // Controls
        private VisualElement _controlsContainer;
        private SearchBar _searchBar;
        private ScrollView _scrollView;

        private readonly Dictionary<string, McpListEntry> _mcpListEntriesByAddress = new();

        public McpsView() : base(nameof(McpsView))
        {
            ConfigureUss(nameof(McpsView));

            AddToClassList("side-view");

            CreateControls();
            CreateScrollView();

            DataStoreManager.Mcps.ListView.DataUpdated += DataUpdatedHandler;
        }

        ~McpsView()
        {
            DataStoreManager.Mcps.ListView.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateControls()
        {
            _controlsContainer = new VisualElement { name = "ControlsContainer" };
            Add(_controlsContainer);

            _searchBar = new SearchBar(SearchHandler);
            _controlsContainer.Add(_searchBar);
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView() { name = "ScrollView" };
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

            DataStoreManager.Mcps.ListView.DataUpdated += DataUpdatedHandler;
        }

        private void DataUpdatedHandler(List<McpData> data)
        {
            _scrollView.Clear();
            foreach (var mcpData in data)
            {
                var entry = new McpListEntry(mcpData, Random.Range(0f, 100f));
                _scrollView.Add(entry);
                _mcpListEntriesByAddress[mcpData.Address] = entry;
            }
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
            foreach (var (address, entry) in _mcpListEntriesByAddress)
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
    }
}