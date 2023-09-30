using System;
using System.Collections.Generic;
using Constants;
using UI.Authentication;
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
using UnityEngine.UIElements;

namespace UI.Main
{
    public class MainController : Singleton<MainController>
    {
        [SerializeField] private AuthenticationUIController _authenticationUiController;

        [SerializeField] private UIDocument _uiDocument;
        private NavigationBar _navigationBar;

        public readonly Dictionary<ViewType, VisualElement> ViewsByViewType = new();

        private ViewType _currentViewType = ViewType.Map;

        protected override void Awake()
        {
            base.Awake();
            QueryElements();
            BindButtons();
            HideAllScreens();
        }

        private void QueryElements()
        {
            _navigationBar = _uiDocument.rootVisualElement.Q<NavigationBar>();

            if (Configs.IS_DESKTOP)
            {
                ViewsByViewType.Add(ViewType.Map, _uiDocument.rootVisualElement.Q<MapView>());
                ViewsByViewType.Add(ViewType.Workers, _uiDocument.rootVisualElement.Q<WorkersView>());
                ViewsByViewType.Add(ViewType.Mcps, _uiDocument.rootVisualElement.Q<McpsView>());
                ViewsByViewType.Add(ViewType.Vehicles, _uiDocument.rootVisualElement.Q<VehiclesView>());
                ViewsByViewType.Add(ViewType.Reporting, _uiDocument.rootVisualElement.Q<ReportingView>());
                ViewsByViewType.Add(ViewType.Messaging, _uiDocument.rootVisualElement.Q<MessagingView>());
                ViewsByViewType.Add(ViewType.Settings, _uiDocument.rootVisualElement.Q<SettingsView>());
            }
            else
            {
                ViewsByViewType.Add(ViewType.Map, _uiDocument.rootVisualElement.Q<MapView>());
                ViewsByViewType.Add(ViewType.Tasks, _uiDocument.rootVisualElement.Q<TasksView>());
                ViewsByViewType.Add(ViewType.Status, _uiDocument.rootVisualElement.Q<StatusView>());
                ViewsByViewType.Add(ViewType.Messaging, _uiDocument.rootVisualElement.Q<MessagingView>());
                ViewsByViewType.Add(ViewType.Settings, _uiDocument.rootVisualElement.Q<SettingsView>());
            }
        }

        private void BindButtons()
        {
            foreach (var (viewType, navigationItem) in _navigationBar.NavigationItemsByViewType)
            {
                navigationItem.RegisterCallback<MouseUpEvent>(_ => FocusView(viewType));
            }
        }

        private void HideAllScreens()
        {
            foreach (var (_, view) in ViewsByViewType)
            {
                if (view != null) view.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
            }
        }

        private void FocusView(ViewType viewType)
        {
            HideCurrentScreenElement();
            _currentViewType = viewType;
            ShowCurrentScreenElement();
        }

        private void ShowCurrentScreenElement()
        {
            if (ViewsByViewType[_currentViewType] == null) return;
            ViewsByViewType[_currentViewType].style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        private void HideCurrentScreenElement()
        {
            if (ViewsByViewType[_currentViewType] == null) return;
            ViewsByViewType[_currentViewType].style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
    }
}