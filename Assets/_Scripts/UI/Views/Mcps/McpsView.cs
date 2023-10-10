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

            DataWatcherManager.Mcps.ListView.DataRequested += UpdateView;
        }

        private void UpdateView()
        {
            _scrollView.Clear();
            foreach (var mcpData in DataWatcherManager.Mcps.ListView.Response.Results)
            {
                _scrollView.Add(new McpListEntry(mcpData, Random.Range(0f, 100f)));
            }
        }
    }
}