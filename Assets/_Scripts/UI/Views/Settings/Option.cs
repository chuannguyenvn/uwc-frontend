using System;
using LocalizationNS;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class Option : AdaptiveElement
    {
        private TextElement _optionNameText;

        public Option(string name, string value, Action callback) : base(name)
        {
            ConfigureUss(nameof(Option));

            CreateOptionNameText(name);

            RegisterCallback<MouseUpEvent>(_ => callback?.Invoke());
        }

        private void CreateOptionNameText(string name)
        {
            _optionNameText = new TextElement() { name = "OptionNameText" };
            _optionNameText.AddToClassList("normal-text");
            _optionNameText.text = Localization.GetSentence(name);
            Add(_optionNameText);
        }

        public void Activate()
        {
            _optionNameText.RemoveFromClassList("deactivated");
            _optionNameText.AddToClassList("activated");
        }

        public void Deactivate()
        {
            _optionNameText.RemoveFromClassList("activated");
            _optionNameText.AddToClassList("deactivated");
        }
    }
}