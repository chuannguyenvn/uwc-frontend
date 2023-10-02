using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Settings
{
    public class SectionHeader : AdaptiveElement
    {
        public TextElement TitleText;
        public VisualElement HeaderLine;

        public SectionHeader(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Settings/SectionHeader"));
            AddToClassList("section-header");

            TitleText = new TextElement { name = "TitleText" };
            TitleText.AddToClassList("sub-text");
            TitleText.AddToClassList("grey-text");
            TitleText.text = name;
            Add(TitleText);

            HeaderLine = new VisualElement { name = "HeaderLine" };
            Add(HeaderLine);
        }
    }
}