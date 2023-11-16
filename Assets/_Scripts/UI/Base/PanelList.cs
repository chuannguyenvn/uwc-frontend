using UnityEngine.UIElements;

namespace UI.Base
{
    public class PanelList : AdaptiveElement
    {
        private ScrollView _scrollView;

        public PanelList() : base(nameof(PanelList))
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