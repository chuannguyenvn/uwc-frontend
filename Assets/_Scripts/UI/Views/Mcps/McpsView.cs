using System.Collections.Generic;
using Commons.Models;
using Requests;
using Requests.DataStores;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace UI.Views.Mcps
{
    public class McpsView : View
    {
        private ScrollView _scrollView;

        public McpsView() : base(nameof(McpsView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Mcps/McpsView"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Mcps/McpListEntry"));
            AddToClassList("side-view");

            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

            DataStoreManager.Mcps.ListView.DataUpdated += DataUpdatedHandler;
        }
        
        ~McpsView()
        {
            DataStoreManager.Mcps.ListView.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(List<McpData> data)
        {
            _scrollView.Clear();
            foreach (var mcpData in data)
            {
                _scrollView.Add(new McpListEntry(mcpData, Random.Range(0f, 100f)));
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
    }
}