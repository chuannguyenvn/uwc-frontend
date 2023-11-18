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
using UI.Views.Vehicles;
using UI.Views.Workers;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class Root : AdaptiveElement
    {
        // Flags to prevent maps from being dragged under UI elements
        public static bool IsMouseOverElement;
        public static bool IsMouseDownElement;

        // Authentication screen
        private AuthenticationScreen _authenticationScreen;

        // Navigation bar
        private NavigationBar _navigationBar;

        // View flags
        private readonly Dictionary<ViewType, View> _viewsByViewType = new();
        private ViewType _mainActiveViewType;

        // Views
        private WorkersView _workersView;
        private McpsView _mcpsView;
        private VehiclesView _vehiclesView;
        private TasksView _tasksView;
        private StatusView _statusView;
        private MessagingView _messagingView;
        private ReportingView _reportingView;
        private SettingsView _settingsView;

        public Root() : base(nameof(Root))
        {
            styleSheets.AddByName("Common");
            styleSheets.AddByName(nameof(Root));

            CreateAuthenticationScreen();
            CreateNavigationBar();
            CreateViews();

            // Default view is Map
            ActivateView(ViewType.Map);

            SubscribeToAuthenticationScreenEvents();
            RegisterMouseEvents();
        }

        ~Root()
        {
            UnsubscribeToAuthenticationScreenEvents();
        }

        private void CreateAuthenticationScreen()
        {
            _authenticationScreen = new AuthenticationScreen();
            Add(_authenticationScreen);
        }

        private void OpenAuthenticationScreen()
        {
            _authenticationScreen.style.display = DisplayStyle.Flex;
            _navigationBar.style.display = DisplayStyle.None;

            foreach (var (_, view) in _viewsByViewType)
            {
                view.style.display = DisplayStyle.None;
            }
        }

        private void CloseAuthenticationScreen()
        {
            _authenticationScreen.style.display = DisplayStyle.None;
            _navigationBar.style.display = DisplayStyle.Flex;
        }

        private void CreateNavigationBar()
        {
            _navigationBar = new NavigationBar();
            Add(_navigationBar);
            _navigationBar.style.display = DisplayStyle.None;
        }

        private void CreateViews()
        {
            if (Configs.IS_DESKTOP)
            {
                _tasksView = new TasksView();
                _workersView = new WorkersView();
                _mcpsView = new McpsView();
                _vehiclesView = new VehiclesView();
                _reportingView = new ReportingView();
                _messagingView = new MessagingView();
                _settingsView = new SettingsView();

                _viewsByViewType.Add(ViewType.Tasks, _tasksView);
                _viewsByViewType.Add(ViewType.Workers, _workersView);
                _viewsByViewType.Add(ViewType.Mcps, _mcpsView);
                _viewsByViewType.Add(ViewType.Vehicles, _vehiclesView);
                _viewsByViewType.Add(ViewType.Reporting, _reportingView);
                _viewsByViewType.Add(ViewType.Messaging, _messagingView);
                _viewsByViewType.Add(ViewType.Settings, _settingsView);
            }
            else
            {
                _tasksView = new TasksView();
                _statusView = new StatusView();
                _messagingView = new MessagingView();
                _settingsView = new SettingsView();

                _viewsByViewType.Add(ViewType.Tasks, _tasksView);
                _viewsByViewType.Add(ViewType.Status, _statusView);
                _viewsByViewType.Add(ViewType.Messaging, _messagingView);
                _viewsByViewType.Add(ViewType.Settings, _settingsView);
            }

            foreach (var (_, view) in _viewsByViewType)
            {
                Add(view);
                view.style.display = DisplayStyle.None;
            }
        }

        public void ActivateView(ViewType viewType, bool asExtension = false)
        {
            foreach (var (type, view) in _viewsByViewType)
            {
                if (!asExtension || type != _mainActiveViewType) view.style.display = DisplayStyle.None;
            }

            if (viewType != ViewType.Map)
            {
                if (viewType == _mainActiveViewType)
                {
                    _viewsByViewType[viewType].style.display = DisplayStyle.None;
                    viewType = ViewType.Map;
                }
                else
                {
                    _viewsByViewType[viewType].style.display = DisplayStyle.Flex;
                }
            }

            _navigationBar.ActivateView(viewType);

            switch (viewType)
            {
                case ViewType.Workers:
                    _workersView.FocusView();
                    break;
                case ViewType.Mcps:
                    _mcpsView.FocusView();
                    break;
                case ViewType.Vehicles:
                    _vehiclesView.FocusView();
                    break;
                case ViewType.Tasks:
                    _tasksView.FocusView();
                    break;
                case ViewType.Status:
                    _statusView.FocusView();
                    break;
                case ViewType.Reporting:
                    _reportingView.FocusView();
                    break;
                case ViewType.Messaging:
                    _messagingView.FocusView();
                    break;
                case ViewType.Settings:
                    _settingsView.FocusView();
                    break;
            }

            if (!asExtension) _mainActiveViewType = viewType;
        }

        private void SubscribeToAuthenticationScreenEvents()
        {
            AuthenticationManager.LoggedIn += CloseAuthenticationScreen;
            AuthenticationManager.LoggedOut += OpenAuthenticationScreen;
        }

        private void UnsubscribeToAuthenticationScreenEvents()
        {
            AuthenticationManager.LoggedIn -= CloseAuthenticationScreen;
            AuthenticationManager.LoggedOut -= OpenAuthenticationScreen;
        }

        private void RegisterMouseEvents()
        {
            RegisterCallback<MouseUpEvent>(evt => { IsMouseDownElement = false; });
            RegisterCallback<MouseOutEvent>(evt => { IsMouseOverElement = false; });
        }

        public new class UxmlFactory : UxmlFactory<Root, UxmlTraits>
        {
        }
    }
}