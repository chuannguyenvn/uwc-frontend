using System;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class AnimatedButton : AdaptiveElement
    {
        public event Action Clicked;
        private bool _isCaptured;

        private VisualElement _iconElement;
        private TextElement _textElement;

        public AnimatedButton(string name = nameof(AnimatedButton)) : base(name)
        {
            ConfigureUss(nameof(AnimatedButton));

            CreateIcon();
            CreateText();
            RegisterCallbacks();
        }

        private void CreateIcon()
        {
            _iconElement = new VisualElement { name = "ButtonIcon" };
            Add(_iconElement);
        }

        private void CreateText()
        {
            _textElement = new TextElement { name = "ButtonText" };
            Add(_textElement);
        }

        private void RegisterCallbacks()
        {
            RegisterCallback<MouseDownEvent>(evt =>
            {
                if (!_isCaptured) return;
                AddToClassList("pressed");
            });

            RegisterCallback<MouseUpEvent>(evt =>
            {
                if (!_isCaptured) return;
                Clicked?.Invoke();
                RemoveFromClassList("pressed");
            });

            RegisterCallback<MouseEnterEvent>(evt => { _isCaptured = true; });

            RegisterCallback<MouseLeaveEvent>(evt =>
            {
                RemoveFromClassList("pressed");
                _isCaptured = false;
            });
        }

        public void SetText(string text)
        {
            _textElement.text = text;
        }

        public void AddToTextClassList(string className)
        {
            _textElement.AddToClassList(className);
        }
    }
}