using System;
using System.Collections.Generic;
using Authentication;
using Settings;
using UI.Authentication;
using UI.Navigation;
using UI.Views.Mcps;
using UI.Views.Messaging;
using UI.Views.Reports;
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

        public WorkersView WorkersView { get; private set; }
        public McpsView McpsView { get; private set; }
        public VehiclesView VehiclesView { get; private set; }
        public TasksView TasksView { get; private set; }
        public StatusView StatusView { get; private set; }
        public MessagingView MessagingView { get; private set; }
        public ReportingView ReportingView { get; private set; }
        public SettingsView SettingsView { get; private set; }

        public Root()
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Common"));
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/Root"));
            AddToClassList(Configs.IS_DESKTOP ? "desktop" : "mobile");

            CreateAuthenticationScreen();
            CreateNavigationBar();
            CreateViews();

            ActivateView(ViewType.Map);

            AuthenticationManager.LoggedIn += CloseAuthenticationScreen;
            AuthenticationManager.LoggedOut += OpenAuthenticationScreen;
        }

        ~Root()
        {
            AuthenticationManager.LoggedIn -= CloseAuthenticationScreen;
            AuthenticationManager.LoggedOut -= OpenAuthenticationScreen;
        }

        private void CreateAuthenticationScreen()
        {
            AuthenticationScreen = new AuthenticationScreen();
            Add(AuthenticationScreen);
        }

        public void OpenAuthenticationScreen()
        {
            AuthenticationScreen.style.display = DisplayStyle.Flex;
            NavigationBar.style.display = DisplayStyle.None;

            foreach (var (_, view) in ViewsByViewType)
            {
                view.style.display = DisplayStyle.None;
            }
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
                WorkersView = new WorkersView();
                McpsView = new McpsView();
                VehiclesView = new VehiclesView();
                ReportingView = new ReportingView();
                MessagingView = new MessagingView();
                SettingsView = new SettingsView();
                
                ViewsByViewType.Add(ViewType.Workers, WorkersView);
                ViewsByViewType.Add(ViewType.Mcps, McpsView);
                ViewsByViewType.Add(ViewType.Vehicles, VehiclesView);
                ViewsByViewType.Add(ViewType.Reporting, ReportingView);
                ViewsByViewType.Add(ViewType.Messaging, MessagingView);
                ViewsByViewType.Add(ViewType.Settings, SettingsView);
            }
            else
            {
                TasksView = new TasksView();
                StatusView = new StatusView();
                MessagingView = new MessagingView();
                SettingsView = new SettingsView();
                
                ViewsByViewType.Add(ViewType.Tasks, TasksView);
                ViewsByViewType.Add(ViewType.Status, StatusView);
                ViewsByViewType.Add(ViewType.Messaging, MessagingView);
                ViewsByViewType.Add(ViewType.Settings, SettingsView);
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
                    McpsView.FocusView();
                    break;
                case ViewType.Vehicles:
                    break;
                case ViewType.Tasks:
                    break;
                case ViewType.Status:
                    break;
                case ViewType.Reporting:
                    ReportingView.FocusView();
                    break;
                case ViewType.Messaging:
                    MessagingView.FocusView();
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