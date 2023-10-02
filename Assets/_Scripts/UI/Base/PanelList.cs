using UnityEngine.UIElements;

namespace UI.Base
{
    public class PanelList : AdaptiveElement
    {
        public ScrollView ScrollView;

        public PanelList() : base(nameof(PanelList))
        {
            ScrollView = new ScrollView();
            Add(ScrollView);
        }
    }
}