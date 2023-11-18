using UnityEngine.UIElements;

namespace UI.Base
{
    public class Popup : Panel
    {
        private VisualElement _controlsContainer;
        private AnimatedButton _closeButton;
        
        private ScrollView _content;

        public Popup()
        {
            ConfigureUss(nameof(Popup));

            AddToClassList("rounded-64px");

            CreateControls();
            CreateContent();
        }

        private void CreateControls()
        {
            _controlsContainer = new VisualElement { name = "ControlsContainer" };
            Add(_controlsContainer);
            
            _closeButton = new AnimatedButton { name = "CloseButton" };
            _closeButton.AddToClassList("white-button");
            _closeButton.AddToClassList("circle-button-64px");
            _closeButton.AddToClassList("close-button");
            _controlsContainer.Add(_closeButton);
        }

        private void CreateContent()
        {
            _content = new ScrollView { name = "Content" };
            Add(_content);
        }

        public new class UxmlFactory : UxmlFactory<Popup, UxmlTraits>
        {
        }
    }
}