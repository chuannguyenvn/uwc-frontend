using UnityEngine.UIElements;

namespace UI.Base
{
    public class PopupInformationEntry : AdaptiveElement
    {
        private TextElement _titleText;
        private TextElement _valueText;

        public PopupInformationEntry(string title) : base(nameof(PopupInformationEntry))
        {
            ConfigureUss(nameof(PopupInformationEntry));

            CreateTitleText(title);
            CreateValueText();
        }

        private void CreateTitleText(string title)
        {
            _titleText = new TextElement { name = "TitleText", text = title };
            _titleText.AddToClassList("grey-text");
            _titleText.AddToClassList("normal-text");
            Add(_titleText);
        }

        private void CreateValueText()
        {
            _valueText = new TextElement { name = "ValueText" };
            _valueText.AddToClassList("black-text");
            _valueText.AddToClassList("normal-text");
            Add(_valueText);
        }

        public void SetValue(string value)
        {
            _valueText.text = value;
        }
    }
}