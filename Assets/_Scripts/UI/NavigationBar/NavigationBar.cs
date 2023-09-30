using System.Collections.Generic;
using Constants;
using UI.Common;
using UI.Main;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI
{
    public class NavigationBar : ResponsiveVisualElement
    {
        public readonly Dictionary<ViewType, NavigationItem> NavigationItemsByViewType = new();

        public NavigationBar() : base("NavigationBar")
        {
            AddToClassList("navigation-bar");
            AddToClassList("colored-background");

            if (Configs.IS_DESKTOP)
            {
                var mapItem = new NavigationItem("Map");
                NavigationItemsByViewType.Add(ViewType.Map, mapItem);
                Add(mapItem);

                var workersItem = new NavigationItem("Workers");
                NavigationItemsByViewType.Add(ViewType.Workers, workersItem);
                Add(workersItem);

                var mcpsItem = new NavigationItem("MCPs");
                NavigationItemsByViewType.Add(ViewType.Mcps, mcpsItem);
                Add(mcpsItem);

                var vehiclesItem = new NavigationItem("Vehicles");
                NavigationItemsByViewType.Add(ViewType.Vehicles, vehiclesItem);
                Add(vehiclesItem);

                var reportingItem = new NavigationItem("Reporting");
                NavigationItemsByViewType.Add(ViewType.Reporting, reportingItem);
                Add(reportingItem);

                var messagingItem = new NavigationItem("Messaging");
                NavigationItemsByViewType.Add(ViewType.Messaging, messagingItem);
                Add(messagingItem);

                var settingsItem = new NavigationItem("Settings");
                NavigationItemsByViewType.Add(ViewType.Settings, settingsItem);
                Add(settingsItem);
            }
            else
            {
                var mapItem = new NavigationItem("Map");
                NavigationItemsByViewType.Add(ViewType.Map, mapItem);
                Add(mapItem);

                var vehiclesItem = new NavigationItem("Tasks");
                NavigationItemsByViewType.Add(ViewType.Tasks, vehiclesItem);
                Add(vehiclesItem);

                var reportingItem = new NavigationItem("Status");
                NavigationItemsByViewType.Add(ViewType.Status, reportingItem);
                Add(reportingItem);

                var messagingItem = new NavigationItem("Messaging");
                NavigationItemsByViewType.Add(ViewType.Messaging, messagingItem);
                Add(messagingItem);

                var settingsItem = new NavigationItem("Settings");
                NavigationItemsByViewType.Add(ViewType.Settings, settingsItem);
                Add(settingsItem);
            }
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