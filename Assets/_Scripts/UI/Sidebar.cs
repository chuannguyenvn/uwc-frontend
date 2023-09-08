using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI
{
    public class Sidebar : VisualElement
    {
        private readonly List<VisualElement> _icons = new();

        private readonly VisualElement _mapIcon;
        private readonly VisualElement _mcpsIcon;
        private readonly VisualElement _messagesIcon;
        private readonly VisualElement _reportIcon;
        private readonly VisualElement _settingsIcon;
        private readonly VisualElement _vehiclesIcon;
        private readonly VisualElement _workersIcon;

        public Sidebar()
        {
            AddToClassList("sidebar");
            AddToClassList("colored-element");

            _mapIcon = new VisualElement { name = "MapIcon" };
            Add(_mapIcon);

            _workersIcon = new VisualElement { name = "WorkersIcon" };
            Add(_workersIcon);

            _mcpsIcon = new VisualElement { name = "MCPsIcon" };
            Add(_mcpsIcon);

            _vehiclesIcon = new VisualElement { name = "VehiclesIcon" };
            Add(_vehiclesIcon);

            _reportIcon = new VisualElement { name = "ReportIcon" };
            Add(_reportIcon);

            _messagesIcon = new VisualElement { name = "MessagesIcon" };
            Add(_messagesIcon);

            _settingsIcon = new VisualElement { name = "SettingsIcon" };
            Add(_settingsIcon);

            _icons = new List<VisualElement>
            {
                _mapIcon,
                _workersIcon,
                _mcpsIcon,
                _vehiclesIcon,
                _reportIcon,
                _messagesIcon,
                _settingsIcon
            };

            foreach (var icon in _icons)
            {
                icon.AddToClassList("sidebar-icon");
                icon.RegisterCallback<MouseDownEvent>(_ => FocusIcon(icon));
            }
        }

        private void FocusIcon(VisualElement icon)
        {
            foreach (var i in _icons) i.RemoveFromClassList("active");

            icon.AddToClassList("active");
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<Sidebar, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}