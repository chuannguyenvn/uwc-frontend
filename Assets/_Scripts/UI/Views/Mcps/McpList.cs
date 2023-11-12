using System.Collections.Generic;
using Commons.Models;
using Requests;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine;

namespace UI.Views.Mcps
{
    public class McpList : AdaptiveElement
    {
        private ScrollView _scrollView;
        public Dictionary<string, McpListEntry> McpListEntriesByAddress = new();

        public McpList() : base(nameof(McpList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Mcps/McpList"));

            _scrollView = new ScrollView() { name = "ScrollView" };
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

            DataStoreManager.Mcps.ListView.DataUpdated += DataUpdatedHandler;
        }

        ~McpList()
        {
            DataStoreManager.Mcps.ListView.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(List<McpData> data)
        {
            _scrollView.Clear();
            foreach (var mcpData in data)
            {
                var entry = new McpListEntry(mcpData, Random.Range(0f, 100f));
                _scrollView.Add(entry);
                McpListEntriesByAddress[mcpData.Address] = entry;
            }
        }
    }
}