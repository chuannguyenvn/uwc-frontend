using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Map
{
    public class NavigationPanel : Panel
    {
        private VisualElement _navigationIcon;

        private VisualElement _textContainer;
        private TextElement _distance;
        private TextElement _roadName;

        public NavigationPanel()
        {
            ConfigureUss(nameof(NavigationPanel));

            AddToClassList("rounded-64px");
            AddToClassList("theme-colored");

            CreateNavigationIcon();
            CreateTextContainer();

            RegisterCallback<ClickEvent>(evt => { GetFirstAncestorOfType<MapView>().ToggleMapMode(); });
        }

        private void CreateNavigationIcon()
        {
            _navigationIcon = new VisualElement { name = "NavigationIcon" };
            Add(_navigationIcon);

            DisableAllNavigationIconClasses();
        }

        private void DisableAllNavigationIconClasses()
        {
            _navigationIcon.EnableInClassList("left", false);
            _navigationIcon.EnableInClassList("slight-left", false);
            _navigationIcon.EnableInClassList("sharp-left", false);
            _navigationIcon.EnableInClassList("right", false);
            _navigationIcon.EnableInClassList("slight-right", false);
            _navigationIcon.EnableInClassList("sharp-right", false);
            _navigationIcon.EnableInClassList("straight", false);
            _navigationIcon.EnableInClassList("roundabout-left", false);
            _navigationIcon.EnableInClassList("roundabout-right", false);
            _navigationIcon.EnableInClassList("u-turn-left", false);
        }

        private void CreateTextContainer()
        {
            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _distance = new TextElement { name = "Distance" };
            _distance.AddToClassList("title-text");
            _distance.AddToClassList("white-text");
            _textContainer.Add(_distance);

            _roadName = new TextElement { name = "RoadName" };
            _roadName.AddToClassList("title-text");
            _roadName.AddToClassList("white-text");
            _textContainer.Add(_roadName);
        }

        public void SetInformation(float distanceInMeters, string instruction)
        {
            DisableAllNavigationIconClasses();

            var turnType = TurnType.Straight;
            if (instruction.ToLower().Contains("left")) turnType = TurnType.Left;
            if (instruction.ToLower().Contains("right")) turnType = TurnType.Right;

            switch (turnType)
            {
                case TurnType.Left:
                    _navigationIcon.EnableInClassList("left", true);
                    break;
                case TurnType.SlightLeft:
                    _navigationIcon.EnableInClassList("slight-left", true);
                    break;
                case TurnType.SharpLeft:
                    _navigationIcon.EnableInClassList("sharp-left", true);
                    break;
                case TurnType.Right:
                    _navigationIcon.EnableInClassList("right", true);
                    break;
                case TurnType.SlightRight:
                    _navigationIcon.EnableInClassList("slight-right", true);
                    break;
                case TurnType.SharpRight:
                    _navigationIcon.EnableInClassList("sharp-right", true);
                    break;
                case TurnType.Straight:
                    _navigationIcon.EnableInClassList("straight", true);
                    break;
                case TurnType.RoundaboutLeft:
                    _navigationIcon.EnableInClassList("roundabout-left", true);
                    break;
                case TurnType.RoundaboutRight:
                    _navigationIcon.EnableInClassList("roundabout-right", true);
                    break;
                case TurnType.UTurnLeft:
                    _navigationIcon.EnableInClassList("u-turn-left", true);
                    break;
            }

            if (distanceInMeters < 1000) _distance.text = $"{distanceInMeters:F0}m";
            else _distance.text = $"{distanceInMeters / 1000:F1}km";
            _roadName.text = instruction;
        }
    }
}