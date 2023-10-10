using System;
using System.Collections.Generic;
using Constants;
using Requests.DataStores;
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

            ActivateView(ViewType.Map);
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

            foreach (var (_, view) in ViewsByViewType)
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

            NavigationBar.ActivateView(viewType);

            switch (viewType)
            {
                case ViewType.Map:
                    break;
                case ViewType.Workers:
                    break;
                case ViewType.Mcps:
                    DataWatcherManager.Mcps.ListView.Focus();
                    break;
                case ViewType.Vehicles:
                    break;
                case ViewType.Tasks:
                    break;
                case ViewType.Status:
                    break;
                case ViewType.Reporting:
                    break;
                case ViewType.Messaging:
                    break;
                case ViewType.Settings:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }

        public new class UxmlFactory : UxmlFactory<Root, UxmlTraits>
        {
        }
    }
}