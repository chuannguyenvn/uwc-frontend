using System.Collections.Generic;
using UI.Main;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI
{
    public class NavigationBar : VisualElement
    {
        public readonly NavigationItem MapButton;
        public readonly NavigationItem WorkersButton;
        public readonly NavigationItem McpsButton;
        public readonly NavigationItem VehiclesButton;
        public readonly NavigationItem ReportingButton;
        public readonly NavigationItem MessagingButton;
        public readonly NavigationItem SettingsButton;
        
        public readonly Dictionary<ViewType, NavigationItem> NavigationItemsByViewType = new ();

        public NavigationBar()
        {
            var stylesheet = Resources.Load<StyleSheet>("Stylesheets/Common/NavigationBar");
            styleSheets.Add(stylesheet);
            
            AddToClassList("navigation-bar");
            AddToClassList("colored-background");

            MapButton = new NavigationItem ("Map");
            NavigationItemsByViewType.Add(ViewType.Map, MapButton);
            Add(MapButton);

            WorkersButton = new NavigationItem ("Workers");
            NavigationItemsByViewType.Add(ViewType.Workers, WorkersButton);
            Add(WorkersButton);

            McpsButton = new NavigationItem ("MCPs");
            NavigationItemsByViewType.Add(ViewType.Mcps, McpsButton);
            Add(McpsButton);

            VehiclesButton = new NavigationItem ("Vehicles");
            NavigationItemsByViewType.Add(ViewType.Vehicles, VehiclesButton);
            Add(VehiclesButton);

            ReportingButton = new NavigationItem ("Reporting");
            NavigationItemsByViewType.Add(ViewType.Reporting, ReportingButton);
            Add(ReportingButton);

            MessagingButton = new NavigationItem ("Messaging");
            NavigationItemsByViewType.Add(ViewType.Messaging, MessagingButton);
            Add(MessagingButton);

            SettingsButton = new NavigationItem ("Settings");
            NavigationItemsByViewType.Add(ViewType.Settings, SettingsButton);
            Add(SettingsButton);
        }
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<NavigationBar, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}