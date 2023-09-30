using Constants;
using UI.Common;
using UI.MCPs;
using UI.Views.Map;
using UI.Views.Messaging;
using UI.Views.Reporting;
using UI.Views.Settings;
using UI.Views.Status;
using UI.Views.Tasks;
using UI.Views.Vehicles;
using UI.Views.Workers;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Main
{
    public class MainView : ResponsiveVisualElement
    {
        public readonly NavigationBar NavigationBar;
        public readonly VisualElement ContentContainer;
        
        public MainView() : base("Main")
        {
            LoadStylesheet("Common");
            AddToClassList("main");
            
            NavigationBar = new NavigationBar();
            Add(NavigationBar);
            
            ContentContainer = new VisualElement { name = "Content" };
            Add(ContentContainer);
            
            if (Configs.IS_DESKTOP)
            {
                ContentContainer.Add(new MapView());
                ContentContainer.Add(new WorkersView());
                ContentContainer.Add(new McpsView());
                ContentContainer.Add(new VehiclesView());
                ContentContainer.Add(new ReportingView());
                ContentContainer.Add(new MessagingView());
                ContentContainer.Add(new SettingsView());
            }
            else
            {
                ContentContainer.Add(new MapView());
                ContentContainer.Add(new TasksView());
                ContentContainer.Add(new StatusView());
                ContentContainer.Add(new MessagingView());
                ContentContainer.Add(new SettingsView());
            }
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