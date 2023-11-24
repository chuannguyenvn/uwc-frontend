using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Map
{
    public class MobileMapModeTogglePanel : Panel
    {
        private AnimatedButton _showAllDestinationsButton;
        private VisualElement _separator;
        private AnimatedButton _showTrafficButton;

        private bool _showAllDestinations;
        private bool _showTraffic;

        public MobileMapModeTogglePanel()
        {
            ConfigureUss(nameof(MobileMapModeTogglePanel));

            AddToClassList("rounded-64px");

            CreateShowAllDestinationsButton();
            CreateSeparator();
            CreateShowTrafficButton();

            SetShowAllDestination(false);
            SetShowTraffic(false);
        }

        private void CreateShowAllDestinationsButton()
        {
            _showAllDestinationsButton = new AnimatedButton("ShowAllDestinationsButton");
            _showAllDestinationsButton.AddToClassList("rounded-64px");
            _showAllDestinationsButton.AddToTextClassList("normal-text");
            _showAllDestinationsButton.SetText("Show all stops");
            Add(_showAllDestinationsButton);

            _showAllDestinationsButton.Clicked += () => { SetShowAllDestination(!_showAllDestinations); };
        }

        private void CreateSeparator()
        {
            _separator = new VisualElement { name = "Separator" };
            Add(_separator);
        }

        private void CreateShowTrafficButton()
        {
            _showTrafficButton = new AnimatedButton("ShowTrafficButton");
            _showTrafficButton.AddToClassList("rounded-64px");
            _showTrafficButton.AddToTextClassList("normal-text");
            _showTrafficButton.SetText("Show traffic");
            Add(_showTrafficButton);

            _showTrafficButton.Clicked += () => { SetShowTraffic(!_showTraffic); };
        }

        private void SetShowAllDestination(bool showAllDestination)
        {
            _showAllDestinations = showAllDestination;
            _showAllDestinationsButton.EnableInClassList("active", _showAllDestinations);
            _showAllDestinationsButton.EnableInClassList("inactive", !_showAllDestinations);
        }

        private void SetShowTraffic(bool showTraffic)
        {
            _showTraffic = showTraffic;
            _showTrafficButton.EnableInClassList("active", _showTraffic);
            _showTrafficButton.EnableInClassList("inactive", !_showTraffic);

            if (OnlineMaps.instance) OnlineMaps.instance.traffic = _showTraffic;
        }
    }
}