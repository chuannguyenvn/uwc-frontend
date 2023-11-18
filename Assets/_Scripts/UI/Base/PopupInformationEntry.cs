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
            Add(_titleText);
        }

        private void CreateValueText()
        {
            _valueText = new TextElement { name = "ValueText" };
            Add(_valueText);
        }

        public void SetValue(string value)
        {
            _valueText.text = value;
        }
    }
}