using System.Collections.Generic;
using Constants;
using UI.Authentication;
using UI.Navigation;
using UI.Views.Mcps;
using UI.Views.Messaging;
using UI.Views.Settings;
using UI.Views.Status;
using UI.Views.Tasks;
using UI.Views.Workers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class Root : VisualElement
    {
        public AuthenticationScreen AuthenticationScreen;

        public NavigationBar NavigationBar;
        public Dictionary<ViewType, View> ViewsByViewType = new();

        public Root()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/Root"));
            AddToClassList(Configs.IS_DESKTOP ? "desktop" : "mobile");

            CreateAuthenticationScreen();
            CreateNavigationBar();
            CreateViews();
        }

        private void CreateAuthenticationScreen()
        {
            AuthenticationScreen = new AuthenticationScreen();
            Add(AuthenticationScreen);
        }

        public void CloseAuthenticationScreen()
        {
            AuthenticationScreen.style.display = DisplayStyle.None;
            NavigationBar.style.display = DisplayStyle.Flex;
        }
        
        private void CreateNavigationBar()
        {
            NavigationBar = new NavigationBar();
            Add(NavigationBar);
            NavigationBar.style.display = DisplayStyle.None;
        }

        private void CreateViews()
        {
            if (Configs.IS_DESKTOP)
            {
                ViewsByViewType.Add(ViewType.Workers, new WorkersView());
                ViewsByViewType.Add(ViewType.Mcps, new McpsView());
                ViewsByViewType.Add(ViewType.Vehicles, new VehiclesView());
                ViewsByViewType.Add(ViewType.Messaging, new MessagingView());
                ViewsByViewType.Add(ViewType.Settings, new SettingsView());
            }
            else
            {
                ViewsByViewType.Add(ViewType.Tasks, new TasksView());
                ViewsByViewType.Add(ViewType.Status, new StatusView());
                ViewsByViewType.Add(ViewType.Messaging, new MessagingView());
                ViewsByViewType.Add(ViewType.Settings, new SettingsView());
            }

            foreach (var (viewType, view) in ViewsByViewType)
            {
                Add(view);
                view.style.display = DisplayStyle.None;
            }
        }

        public void ActivateView(ViewType viewType)
        {
            foreach (var (type, view) in ViewsByViewType)
            {
                view.style.display = type == viewType ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        public new class UxmlFactory : UxmlFactory<Root, UxmlTraits>
        {
        }
    }
}