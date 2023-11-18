﻿using UnityEngine.UIElements;

namespace UI.Base
{
    public class FullscreenPopup : View
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
            
            _background.PlaceBehind(_popupContainer);
        }

        private void CreateBackground()
        {
            _background = new VisualElement { name = "Background" };
            Add(_background);
        }

        private void CreatePopupContainer()
        {
            _popupContainer = new Panel { name = "PopupContainer" };
            _popupContainer.AddToClassList("rounded-64px");
            Add(_popupContainer);
        }

        private void CreateControls()
        {
            _controlsContainer = new VisualElement { name = "ControlsContainer" };
            _popupContainer.Add(_controlsContainer);

            _closeButton = new AnimatedButton { name = "CloseButton" };
            _closeButton.AddToClassList("white-button");
            _closeButton.AddToClassList("circle-button-64px");
            _closeButton.AddToClassList("close-button");
            _controlsContainer.Add(_closeButton);
        }

        private void CreateContent()
        {
            _content = new ScrollView { name = "Content" };
            _popupContainer.Add(_content);
        }

        public new class UxmlFactory : UxmlFactory<FullscreenPopup, UxmlTraits>
        {
        }
    }
}