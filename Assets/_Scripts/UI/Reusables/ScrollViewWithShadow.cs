using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables
{
    public class ScrollViewWithShadow : AdaptiveElement
    {
        private VisualElement _shadow;
        public ScrollView ScrollView;

        public ScrollViewWithShadow(ShadowType shadowType) : base(nameof(ScrollViewWithShadow))
        {
            ConfigureUss(nameof(ScrollViewWithShadow));

            CreateShadow(shadowType);
            CreateScrollView();
        }

        private void CreateShadow(ShadowType shadowType)
        {
            _shadow = new VisualElement { name = "Shadow" };
            _shadow.AddToClassList("shadow");
            Add(_shadow);

            _shadow.pickingMode = PickingMode.Ignore;

            switch (shadowType)
            {
                case ShadowType.InnerTop:
                    _shadow.AddToClassList("inner-top");
                    break;
                case ShadowType.InnerBottom:
                    _shadow.AddToClassList("inner-bottom");
                    break;
                case ShadowType.InnerLeft:
                    _shadow.AddToClassList("inner-left");
                    break;
                case ShadowType.InnerRight:
                    _shadow.AddToClassList("inner-right");
                    break;
            }
        }

        private void CreateScrollView()
        {
            ScrollView = new ScrollView { name = "ScrollView" };
            Add(ScrollView);
        }

        public void AddToScrollView(VisualElement element)
        {
            ScrollView.Add(element);
            _shadow.BringToFront();
        }

        public new void Clear()
        {
            ScrollView.Clear();
        }
    }

    public enum ShadowType
    {
        InnerTop,
        InnerBottom,
        InnerLeft,
        InnerRight,
    }
}