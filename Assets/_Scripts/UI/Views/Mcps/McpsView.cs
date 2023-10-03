using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

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

            for (int i = 0; i < 30; i++)
            {
                _scrollView.Add(new McpListEntry(new McpData()
                {
                    Address = "Address placeholder",
                }, Random.Range(0f, 100f)));
            }
        }
    }
}