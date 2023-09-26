using UI.MCPs;
using UI.Messaging;
using UI.Reporting;
using UI.Settings;
using UI.Vehicles;
using UI.Workers;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Main
{
    public class MainView : VisualElement
    {
        public readonly Sidebar Sidebar;

        public readonly VisualElement ContentContainer;
        public readonly WorkersView WorkersView;
        public readonly McpsScreen McpsScreen;
        public readonly VehiclesScreen VehiclesScreen;
        public readonly ReportingView ReportingView;
        public readonly MessagingView MessagingView;
        public readonly SettingsScreen SettingsScreen;
        
        public MainView()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common/Main"));
            
            AddToClassList("main");
            
            Sidebar = new Sidebar();
            Add(Sidebar);
            
            ContentContainer = new VisualElement { name = "Container" };
            Add(ContentContainer);
            
            WorkersView = new WorkersView();
            ContentContainer.Add(WorkersView);
            
            McpsScreen = new McpsScreen();
            ContentContainer.Add(McpsScreen);
            
            VehiclesScreen = new VehiclesScreen();
            ContentContainer.Add(VehiclesScreen);
            
            ReportingView = new ReportingView();
            ContentContainer.Add(ReportingView);
            
            MessagingView = new MessagingView();
            ContentContainer.Add(MessagingView);
            
            SettingsScreen = new SettingsScreen();
            ContentContainer.Add(SettingsScreen);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MainView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}