using System;
using UI.Common;
using UnityEngine.UIElements;

namespace UI
{
    public class NavigationItem : ResponsiveVisualElement
    {
        private readonly VisualElement _icon;
        private readonly TextElement _name;
        
        public NavigationItem(string name) : base(name)
        {
            AddToClassList("navigation-item");
            
            _icon = new VisualElement { name = "Icon" };
            _icon.AddToClassList("icon");
            Add(_icon);

            _name = new TextElement { name = "Name" };
            _name.AddToClassList("name");
            Add(_name);
        }
        
        private void Select()
        {
            AddToClassList("active");
        }

        private void Deselect()
        {
            RemoveFromClassList("active");
        }
    }
}