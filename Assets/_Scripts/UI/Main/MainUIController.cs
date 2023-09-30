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
    public class MainUIController : Singleton<MainUIController>
    {
        [SerializeField] private AuthenticationUIController _authenticationUiController;

        [SerializeField] private UIDocument _uiDocument;
        private NavigationBar _navigationBar;

        private readonly Dictionary<ViewType, VisualElement> _viewsByViewType = new();

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
                _viewsByViewType.Add(ViewType.Map, _uiDocument.rootVisualElement.Q<MapView>());
                _viewsByViewType.Add(ViewType.Workers, _uiDocument.rootVisualElement.Q<WorkersView>());
                _viewsByViewType.Add(ViewType.Mcps, _uiDocument.rootVisualElement.Q<McpsView>());
                _viewsByViewType.Add(ViewType.Vehicles, _uiDocument.rootVisualElement.Q<VehiclesView>());
                _viewsByViewType.Add(ViewType.Reporting, _uiDocument.rootVisualElement.Q<ReportingView>());
                _viewsByViewType.Add(ViewType.Messaging, _uiDocument.rootVisualElement.Q<MessagingView>());
                _viewsByViewType.Add(ViewType.Settings, _uiDocument.rootVisualElement.Q<SettingsView>());
            }
            else
            {
                _viewsByViewType.Add(ViewType.Map, _uiDocument.rootVisualElement.Q<MapView>());
                _viewsByViewType.Add(ViewType.Tasks, _uiDocument.rootVisualElement.Q<TasksView>());
                _viewsByViewType.Add(ViewType.Status, _uiDocument.rootVisualElement.Q<StatusView>());
                _viewsByViewType.Add(ViewType.Messaging, _uiDocument.rootVisualElement.Q<MessagingView>());
                _viewsByViewType.Add(ViewType.Settings, _uiDocument.rootVisualElement.Q<SettingsView>());
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
            foreach (var (_, view) in _viewsByViewType)
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
            if (_viewsByViewType[_currentViewType] == null) return;
            _viewsByViewType[_currentViewType].style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);
        }

        private void HideCurrentScreenElement()
        {
            if (_viewsByViewType[_currentViewType] == null) return;
            _viewsByViewType[_currentViewType].style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
        }
    }
}