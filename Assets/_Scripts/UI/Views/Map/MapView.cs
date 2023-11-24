using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Map
{
    public class MapView : View
    {
        private NavigationPanel _navigationPanel;
        private NextStopPanel _nextStopPanel;
        private MobileMapModeTogglePanel _mobileMapModeTogglePanel;

        private MapMode _currentMapMode = MapMode.NextStop;

        public MapView() : base(nameof(MapView))
        {
            ConfigureUss(nameof(MapView));

            AddToClassList("full-view");
            style.backgroundColor = new StyleColor(new Color(0.0f, 0.0f, 0.0f, 0.0f));
            pickingMode = PickingMode.Ignore;

            CreateNavigationPanel();
            CreateNextStopPanel();
            CreateMobileMapModeTogglePanel();

            ToggleMapMode();
        }

        private void CreateNavigationPanel()
        {
            _navigationPanel = new NavigationPanel();
            _navigationPanel.SetInformation(TurnType.Left, 50, "Tô Hiến Thành");
            Add(_navigationPanel);
        }

        private void CreateNextStopPanel()
        {
            _nextStopPanel = new NextStopPanel();
            _nextStopPanel.SetNextStopAddress("495/4/8 Tô Hiến Thành");
            Add(_nextStopPanel);
        }

        private void CreateMobileMapModeTogglePanel()
        {
            _mobileMapModeTogglePanel = new MobileMapModeTogglePanel();
            Add(_mobileMapModeTogglePanel);
        }

        public void ToggleMapMode()
        {
            if (_currentMapMode == MapMode.Navigation)
            {
                _currentMapMode = MapMode.NextStop;
                _nextStopPanel.style.display = DisplayStyle.Flex;
                _navigationPanel.style.display = DisplayStyle.None;
            }
            else
            {
                _currentMapMode = MapMode.Navigation;
                _nextStopPanel.style.display = DisplayStyle.None;
                _navigationPanel.style.display = DisplayStyle.Flex;
            }
        }
    }
}