using System;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class AnimatedButton : AdaptiveElement
    {
        public event Action Clicked;
        private bool _isCaptured;

        private TextElement _iconOrTextElement;

        public AnimatedButton(string name = nameof(AnimatedButton)) : base(name)
        {
            ConfigureUss(nameof(AnimatedButton));

            CreateIcon();
            RegisterCallbacks();
        }

        private void CreateIcon()
        {
            _iconOrTextElement = new TextElement { name = "ButtonIcon" };
            Add(_iconOrTextElement);
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
            _iconOrTextElement.text = text;
        }
        
        public void AddToTextClassList(string className)
        {
            _iconOrTextElement.AddToClassList(className);
        }
    }
}