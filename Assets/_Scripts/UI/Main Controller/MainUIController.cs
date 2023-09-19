using System;
using UI.Authentication;
using UI.MCPs;
using UI.Messages;
using UI.Reports;
using UI.Settings;
using UI.Vehicles;
using UI.Workers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.MainController
{
    public class MainUIController : Singleton<MainUIController>
    {
        [SerializeField] private AuthenticationUIController _authenticationUiController;

        [SerializeField] private UIDocument _uiDocument;
        private Sidebar _sidebar;
        private WorkersScreen _workersScreen;
        private McpsScreen _mcpsScreen;
        private VehiclesScreen _vehiclesScreen;
        private ReportsScreen _reportsScreen;
        private MessagesScreen _messagesScreen;
        private SettingsScreen _settingsScreen;

        private VisualElement _currentScreenElement;
        private Screen _currentScreen = Screen.Map;

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
            _workersScreen = _uiDocument.rootVisualElement.Q<WorkersScreen>();
            _mcpsScreen = _uiDocument.rootVisualElement.Q<McpsScreen>();
            _vehiclesScreen = _uiDocument.rootVisualElement.Q<VehiclesScreen>();
            _reportsScreen = _uiDocument.rootVisualElement.Q<ReportsScreen>();
            _messagesScreen = _uiDocument.rootVisualElement.Q<MessagesScreen>();
            _settingsScreen = _uiDocument.rootVisualElement.Q<SettingsScreen>();
        }

        private void BindButtons()
        {
            _sidebar.MapButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Map));
            _sidebar.WorkersButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Workers));
            _sidebar.McpsButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Mcps));
            _sidebar.VehiclesButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Vehicles));
            _sidebar.ReportButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Reports));
            _sidebar.MessagesButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Messages));
            _sidebar.SettingsButton.RegisterCallback<MouseUpEvent>(_ => FocusScreen(Screen.Settings));
        }

        private void HideAllScreens()
        {
            _workersScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _mcpsScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _vehiclesScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _reportsScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _messagesScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            _settingsScreen.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }

        private void FocusScreen(Screen screen)
        {
            Debug.Log("Focusing: " + screen);

            HideCurrentScreenElement();
            _currentScreenElement = GetScreenElement(screen);
            ShowCurrentScreenElement();

            _currentScreen = screen;
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

        private VisualElement GetScreenElement(Screen screen)
        {
            return screen switch
            {
                Screen.Map => null,
                Screen.Workers => _workersScreen,
                Screen.Mcps => _mcpsScreen,
                Screen.Vehicles => _vehiclesScreen,
                Screen.Reports => _reportsScreen,
                Screen.Messages => _messagesScreen,
                Screen.Settings => _settingsScreen,
                _ => throw new ArgumentOutOfRangeException(nameof(screen), screen, null)
            };
        }
    }
}