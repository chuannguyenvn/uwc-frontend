using Maps;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Map
{
    public class MobileMapModeTogglePanel : Panel
    {
        private AnimatedButton _focusButton;
        private VisualElement _separator;
        private AnimatedButton _showTrafficButton;

        private bool _focus;
        private bool _showTraffic;

        public MobileMapModeTogglePanel()
        {
            ConfigureUss(nameof(MobileMapModeTogglePanel));

            AddToClassList("rounded-64px");

            CreateFocusButton();
            CreateSeparator();
            CreateShowTrafficButton();

            SetFocusMode(false);
            SetShowTraffic(false);
        }

        private void CreateFocusButton()
        {
            _focusButton = new AnimatedButton("FocusButton");
            _focusButton.AddToClassList("rounded-64px");
            _focusButton.AddToTextClassList("normal-text");
            _focusButton.SetText("Focus mode");
            Add(_focusButton);

            _focusButton.Clicked += () => { SetFocusMode(!_focus); };
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

        private void SetFocusMode(bool focus)
        {
            _focus = focus;
            _focusButton.EnableInClassList("active", _focus);
            _focusButton.EnableInClassList("inactive", !_focus);

            MapDrawer.Instance.FocusModeOn = focus;
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