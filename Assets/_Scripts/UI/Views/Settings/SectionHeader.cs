using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SectionHeader : AdaptiveElement
    {
        private TextElement _titleText;
        private VisualElement _headerLine;

        public SectionHeader(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/SectionHeader"));
            AddToClassList("section-header");

            CreateTitleText(name);
            CreateHeaderLine();
        }

        private void CreateTitleText(string name)
        {
            _titleText = new TextElement { name = "TitleText" };
            _titleText.AddToClassList("sub-text");
            _titleText.AddToClassList("grey-text");
            _titleText.text = name;
            Add(_titleText);
        }

        private void CreateHeaderLine()
        {
            _headerLine = new VisualElement { name = "HeaderLine" };
            Add(_headerLine);
        }
    }
}