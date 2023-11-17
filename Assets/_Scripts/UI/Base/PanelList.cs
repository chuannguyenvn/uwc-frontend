using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class PanelList : AdaptiveElement
    {
        private ScrollView _scrollView;

        public PanelList() : base(nameof(PanelList))
        {
            ConfigureUss(nameof(PanelList));

            CreateScrollView();
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollView();
            Add(_scrollView);
        }

        public void AddPanel(Panel panel)
        {
            _scrollView.Add(panel);
        }
    }
}