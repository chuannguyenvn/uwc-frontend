using UnityEngine.UIElements;

namespace UI.Base
{
    public abstract class FullscreenPopup : View
    {
        private VisualElement _background;

        private Panel _popupContainer;

        private VisualElement _controlsContainer;
        private AnimatedButton _closeButton;

        private ScrollView _content;

        public FullscreenPopup() : base(nameof(FullscreenPopup))
        {
            ConfigureUss(nameof(FullscreenPopup));

            CreateBackground();
            CreatePopupContainer();
            CreateControls();
            CreateContent();

            _background.SendToBack();
            _closeButton.Clicked += Hide;
            _background.RegisterCallback<ClickEvent>(_ => Hide());

            Hide();
        }

        private void CreateBackground()
        {
            _background = new VisualElement { name = "Background" };
            Add(_background);
        }

        private void CreatePopupContainer()
        {
            _popupContainer = new Panel { name = "PopupContainer" };
            _popupContainer.AddToClassList("rounded-32px");
            Add(_popupContainer);
        }

        private void CreateControls()
        {
            _controlsContainer = new VisualElement { name = "ControlsContainer" };
            _popupContainer.Add(_controlsContainer);

            _closeButton = new AnimatedButton { name = "CloseButton" };
            _closeButton.AddToClassList("white-button");
            _closeButton.AddToClassList("circle-button-48px");
            _closeButton.AddToClassList("close-button");
            _controlsContainer.Add(_closeButton);
        }

        private void CreateContent()
        {
            _content = new ScrollView { name = "Content" };
            _popupContainer.Add(_content);
        }

        public void Show()
        {
            style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            style.display = DisplayStyle.None;
        }

        protected void AddContent(VisualElement element)
        {
            _content.Add(element);
        }
    }
}