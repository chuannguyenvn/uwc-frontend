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
        public readonly NavigationBar NavigationBar;

        public readonly VisualElement ContentContainer;
        public readonly WorkersView WorkersView;
        public readonly McpsView McpsView;
        public readonly VehiclesView VehiclesView;
        public readonly ReportingView ReportingView;
        public readonly MessagingView MessagingView;
        public readonly SettingsView SettingsView;
        
        public MainView()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common/Main"));
            
            AddToClassList("main");
            
            NavigationBar = new NavigationBar();
            Add(NavigationBar);
            
            ContentContainer = new VisualElement { name = "Container" };
            Add(ContentContainer);
            
            WorkersView = new WorkersView();
            ContentContainer.Add(WorkersView);
            
            McpsView = new McpsView();
            ContentContainer.Add(McpsView);
            
            VehiclesView = new VehiclesView();
            ContentContainer.Add(VehiclesView);
            
            ReportingView = new ReportingView();
            ContentContainer.Add(ReportingView);
            
            MessagingView = new MessagingView();
            ContentContainer.Add(MessagingView);
            
            SettingsView = new SettingsView();
            ContentContainer.Add(SettingsView);
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