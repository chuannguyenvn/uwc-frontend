using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Navigation
{
    public class NavigationItem : AdaptiveElement
    {
        private VisualElement _icon;
        private Label _label;

        public NavigationItem(ViewType viewType) : base(nameof(NavigationItem))
        {
            ConfigureUss(nameof(NavigationItem));

            name = viewType.ToString();

            CreateIcon();
            CreateLabel(viewType);

            RegisterCallback<MouseUpEvent>(_ => { GetFirstAncestorOfType<Root>().ActivateView(viewType); });
        }

        private void CreateIcon()
        {
            _icon = new VisualElement { name = "Icon" };
            _icon.AddToClassList("icon");
            Add(_icon);
        }

        private void CreateLabel(ViewType viewType)
        {
            _label = new Label { name = "Label" };
            _label.AddToClassList("sub-text");
            _label.AddToClassList("white-text");
            _label.text = viewType.ToString();
            Add(_label);
        }
    }
}