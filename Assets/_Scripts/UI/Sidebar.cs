using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI
{
    public class Sidebar : VisualElement
    {
        private readonly List<VisualElement> _buttons = new();

        public readonly VisualElement MapButton;
        public readonly VisualElement WorkersButton;
        public readonly VisualElement McpsButton;
        public readonly VisualElement VehiclesButton;
        public readonly VisualElement ReportButton;
        public readonly VisualElement MessagesButton;
        public readonly VisualElement SettingsButton;

        public Sidebar()
        {
            var stylesheet = Resources.Load<StyleSheet>("Stylesheets/Common/Sidebar");
            styleSheets.Add(stylesheet);
            
            AddToClassList("sidebar");
            AddToClassList("colored-background");

            MapButton = new VisualElement { name = "MapButton" };
            Add(MapButton);

            WorkersButton = new VisualElement { name = "WorkersButton" };
            Add(WorkersButton);

            McpsButton = new VisualElement { name = "MCPsButton" };
            Add(McpsButton);

            VehiclesButton = new VisualElement { name = "VehiclesButton" };
            Add(VehiclesButton);

            ReportButton = new VisualElement { name = "ReportsButton" };
            Add(ReportButton);

            MessagesButton = new VisualElement { name = "MessagesButton" };
            Add(MessagesButton);

            SettingsButton = new VisualElement { name = "SettingsButton" };
            Add(SettingsButton);

            _buttons = new List<VisualElement>
            {
                MapButton,
                WorkersButton,
                McpsButton,
                VehiclesButton,
                ReportButton,
                MessagesButton,
                SettingsButton
            };

            foreach (var button in _buttons)
            {
                button.AddToClassList("icon");
                button.RegisterCallback<MouseDownEvent>(_ => FocusButton(button));
            }
        }

        private void FocusButton(VisualElement button)
        {
            foreach (var i in _buttons) i.RemoveFromClassList("active");

            button.AddToClassList("active");
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