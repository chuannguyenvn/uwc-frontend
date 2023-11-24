using UI.Base;

namespace UI.Views.Map
{
    public class MapView : View
    {
        private NavigationPanel _navigationPanel;
        private NextStopPanel _nextStopPanel;
        private MobileMapModeToggle _mobileMapModeToggle;

        public MapView() : base(nameof(MapView))
        {
            ConfigureUss(nameof(MapView));

            _navigationPanel = new NavigationPanel();
            Add(_navigationPanel);

            _nextStopPanel = new NextStopPanel();
            Add(_nextStopPanel);

            _mobileMapModeToggle = new MobileMapModeToggle();
            Add(_mobileMapModeToggle);
        }
    }
}