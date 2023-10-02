using Constants;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Navigation
{
    public class NavigationItem : AdaptiveElement
    {
        private readonly VisualElement _icon;
        private readonly Label _label;

        public NavigationItem(ViewType viewType) : base(nameof(NavigationItem))
        {
            name = viewType.ToString();
            AddToClassList("navigation-item");

            _icon = new VisualElement { name = "Icon" };
            _icon.AddToClassList("icon");
            Add(_icon);
            
            _label = new Label { name = "Label" };
            _label.AddToClassList("label");
            _label.AddToClassList("white-text");
            _label.text = viewType.ToString();
            Add(_label);
        }
    }
}