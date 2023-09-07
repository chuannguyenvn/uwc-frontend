using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI
{
    public class Sidebar : VisualElement
    {
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

        private readonly VisualElement _mapIcon;
        private readonly VisualElement _workersIcon;
        private readonly VisualElement _mcpsIcon;
        private readonly VisualElement _vehiclesIcon;
        private readonly VisualElement _reportIcon;
        private readonly VisualElement _messagesIcon;
        private readonly VisualElement _settingsIcon;

        public Sidebar()
        {
            AddToClassList("sidebar");

            _mapIcon = new VisualElement() { name = "MapIcon" };
            _mapIcon.AddToClassList("icon");
            Add(_mapIcon);

            _workersIcon = new VisualElement() { name = "WorkersIcon" };
            _workersIcon.AddToClassList("icon");
            Add(_workersIcon);

            _mcpsIcon = new VisualElement() { name = "MCPsIcon" };
            _mcpsIcon.AddToClassList("icon");
            Add(_mcpsIcon);

            _vehiclesIcon = new VisualElement() { name = "VehiclesIcon" };
            _vehiclesIcon.AddToClassList("icon");
            Add(_vehiclesIcon);

            _reportIcon = new VisualElement() { name = "ReportIcon" };
            _reportIcon.AddToClassList("icon");
            Add(_reportIcon);

            _messagesIcon = new VisualElement() { name = "MessagesIcon" };
            _messagesIcon.AddToClassList("icon");
            Add(_messagesIcon);

            _settingsIcon = new VisualElement() { name = "SettingsIcon" };
            _settingsIcon.AddToClassList("icon");
            Add(_settingsIcon);
        }
    }
}