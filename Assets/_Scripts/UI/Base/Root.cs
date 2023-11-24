using System.Collections.Generic;
using Authentication;
using Commons.Types.SettingOptions;
using LocalizationNS;
using Settings;
using UI.Authentication;
using UI.Navigation;
using UI.Reusables.ChatBubbles;
using UI.Views.Map;
using UI.Views.Mcps;
using UI.Views.Messaging;
using UI.Views.Reports;
using UI.Views.Settings;
using UI.Views.Status;
using UI.Views.Tasks;
using UI.Views.Vehicles;
using UI.Views.Workers;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class Root : AdaptiveElement
    {
        public static Root Instance;

        // Flags to prevent maps from being dragged under UI elements
        public static bool IsMouseOverElement;
        public static bool IsMouseDownElement;

        // Authentication screen
        private AuthenticationScreen _authenticationScreen;

        // Navigation bar
        private NavigationBar _navigationBar;

        // View flags
        private readonly Dictionary<ViewType, View> _viewsByViewType = new();
        private ViewType _mainActiveViewType = ViewType.None;

        // Views
        private MapView _mapView;
        private WorkersView _workersView;
        private McpsView _mcpsView;
        private VehiclesView _vehiclesView;
        private TasksView _tasksView;
        private StatusView _statusView;
        private MessagingView _messagingView;
        private ReportingView _reportingView;
        private SettingsView _settingsView;

        // Chat bubbles
        private ChatBubblesPanel _chatBubblesPanel;

        private VisualElement _fullscreenPopupContainer;
        private List<FullscreenPopup> _popups = new List<FullscreenPopup>();


        // For exiting app in Android
        private float _timeSinceLastBackButtonPress;

        public Root() : base(nameof(Root), false)
        {
            Instance = this;

            RestoreLanguageOption();

            styleSheets.AddByName("Common");
            styleSheets.AddByName(nameof(Root));
            pickingMode = PickingMode.Ignore;

            if (Configs.IS_DESKTOP) CreateChatBubblesPanel();
            CreateAuthenticationScreen();
            CreateNavigationBar();
            CreateViews();
            CreatePopups();

            SubscribeToAuthenticationScreenEvents();
            RegisterMouseEvents();
            if (!Configs.IS_DESKTOP) SubscribeToGlobalBackButtonClickEvent();
        }

        ~Root()
        {
            UnsubscribeToAuthenticationScreenEvents();
        }

        private static void RestoreLanguageOption()
        {
            if (PlayerPrefs.GetString("Language", LanguageOption.English.ToString()) == LanguageOption.English.ToString())
            {
                Localization.LanguageOption = LanguageOption.English;
            }
            else
            {
                Localization.LanguageOption = LanguageOption.Vietnamese;
            }
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

            // Default view is Map
            ActivateView(ViewType.Map);
        }

        private void CreateChatBubblesPanel()
        {
            _chatBubblesPanel = new ChatBubblesPanel();
            Add(_chatBubblesPanel);
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
                _mapView = new MapView();
                _tasksView = new TasksView();
                _workersView = new WorkersView();
                _mcpsView = new McpsView();
                _vehiclesView = new VehiclesView();
                _reportingView = new ReportingView();
                _messagingView = new MessagingView();
                _settingsView = new SettingsView();

                _viewsByViewType.Add(ViewType.Map, _mapView);
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
                _mapView = new MapView();
                _tasksView = new TasksView();
                _statusView = new StatusView();
                _messagingView = new MessagingView();
                _settingsView = new SettingsView();

                _viewsByViewType.Add(ViewType.Map, _mapView);
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

        private void CreatePopups()
        {
            _fullscreenPopupContainer = new VisualElement { name = "FullscreenPopupContainer" };
            Add(_fullscreenPopupContainer);
            _fullscreenPopupContainer.BringToFront();
            _fullscreenPopupContainer.pickingMode = PickingMode.Ignore;

            foreach (var popup in _popups)
            {
                _fullscreenPopupContainer.Add(popup);
                popup.BringToFront();
            }
        }

        public void ActivateView(ViewType viewType, bool asExtension = false)
        {
            foreach (var (type, view) in _viewsByViewType)
            {
                if (!asExtension || type != _mainActiveViewType) view.style.display = DisplayStyle.None;
            }

            if (viewType == _mainActiveViewType)
            {
                _viewsByViewType[viewType].style.display = DisplayStyle.None;
                viewType = ViewType.Map;
            }
            else
            {
                _viewsByViewType[viewType].style.display = DisplayStyle.Flex;
            }

            _navigationBar.ActivateView(viewType);

            switch (viewType)
            {
                case ViewType.Map:
                    _mapView.FocusView();
                    break;
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

        private void SubscribeToGlobalBackButtonClickEvent()
        {
            RootController.BackButtonPressed += () =>
            {
                if (_mainActiveViewType == ViewType.Map)
                {
                    if (Time.time - _timeSinceLastBackButtonPress < 3)
                    {
                        Application.Quit();
                    }

                    _timeSinceLastBackButtonPress = Time.time;
                    AndroidUtility.ShowAndroidToastMessage(Localization.GetSentence(Sentence.MapView.PRESS_BACK_AGAIN_TO_EXIT));
                }
                else
                {
                    ActivateView(ViewType.Map);
                }
            };
        }

        public void AddPopup(FullscreenPopup popup)
        {
            _popups.Add(popup);
        }

        public new class UxmlFactory : UxmlFactory<Root, UxmlTraits>
        {
        }
    }
}