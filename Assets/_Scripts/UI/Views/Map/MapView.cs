using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Map
{
    public class MapView : View
    {
        private NavigationPanel _navigationPanel;
        private NextStopPanel _nextStopPanel;
        private MobileMapModeToggle _mobileMapModeToggle;

        private MapMode _currentMapMode = MapMode.NextStop;

        public MapView() : base(nameof(MapView))
        {
            ConfigureUss(nameof(MapView));

            AddToClassList("full-view");

            CreateNavigationPanel();
            CreateNextStopPanel();
            CreateMobileMapModeToggleButton();

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
            Add(_nextStopPanel);
        }

        private void CreateMobileMapModeToggleButton()
        {
            _mobileMapModeToggle = new MobileMapModeToggle();
            _mobileMapModeToggle.Clicked += ToggleMapMode;
            Add(_mobileMapModeToggle);
        }

        private void ToggleMapMode()
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