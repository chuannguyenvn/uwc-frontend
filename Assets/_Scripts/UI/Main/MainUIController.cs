using System;
using UI.Authentication;
using UI.MCPs;
using UI.Messaging;
using UI.Reporting;
using UI.Settings;
using UI.Vehicles;
using UI.Workers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Main
{
    public class MainUIController : Singleton<MainUIController>
    {
        [SerializeField] private AuthenticationUIController _authenticationUiController;

        [SerializeField] private UIDocument _uiDocument;
        private Sidebar _sidebar;
        private WorkersView _workersView;
        private McpsScreen _mcpsScreen;
        private VehiclesScreen _vehiclesScreen;
        private ReportingView _reportingView;
        private MessagingView _messagingView;
        private SettingsScreen _settingsScreen;

        private VisualElement _currentScreenElement;
        private View _currentView = View.Map;

        protected override void Awake()
        {
            base.Awake();
            QueryElements();
            BindButtons();
            HideAllScreens();
        }

        private void QueryElements()
        {
            _sidebar = _uiDocument.rootVisualElement.Q<Sidebar>();
            _workersView = _uiDocument.rootVisualElement.Q<WorkersView>();
            _mcpsScreen = _uiDocument.rootVisualElement.Q<McpsScreen>();
            _vehiclesScreen = _uiDocument.rootVisualElement.Q<VehiclesScreen>();
            _reportingView = _uiDocument.rootVisualElement.Q<ReportingView>();
            _messagingView = _uiDocument.rootVisualElement.Q<MessagingView>();
            _settingsScreen = _uiDocument.rootVisualElement.Q<SettingsScreen>();
        }

        private void BindButtons()
        {
            _sidebar.MapButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Map));
            _sidebar.WorkersButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Workers));
            _sidebar.McpsButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Mcps));
            _sidebar.VehiclesButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Vehicles));
            _sidebar.ReportButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Reports));
            _sidebar.MessagesButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Messages));
            _sidebar.SettingsButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(View.Settings));
        }

        private void HideAllScreens()
        {
            _workersView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _mcpsScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _vehiclesScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _reportingView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _messagingView.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _settingsScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        private void FocusScreen(View view)
        {
            Debug.Log("Focusing: " + view);

            HideCurrentScreenElement();
            _currentScreenElement = GetScreenElement(view);
            ShowCurrentScreenElement();

            _currentView = view;
        }

        private void ShowCurrentScreenElement()
        {
            if (_currentScreenElement == null) return;
            _currentScreenElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        private void HideCurrentScreenElement()
        {
            if (_currentScreenElement == null) return;
            _currentScreenElement.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        private VisualElement GetScreenElement(View view)
        {
            return view switch
            {
                View.Map => null,
                View.Workers => _workersView,
                View.Mcps => _mcpsScreen,
                View.Vehicles => _vehiclesScreen,
                View.Reports => _reportingView,
                View.Messages => _messagingView,
                View.Settings => _settingsScreen,
                _ => throw new ArgumentOutOfRangeException(nameof(view), view, null)
            };
        }
    }
}