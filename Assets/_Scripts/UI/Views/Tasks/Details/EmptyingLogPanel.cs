using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class EmptyingLogPanel : Panel
    {
        public TextElement TitleText;

        public EmptyingLogPanel() : base(nameof(EmptyingLogPanel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/EmptyingLogPanel"));
            AddToClassList("rounded-32px");

            TitleText = new TextElement { name = "TitleText" };
            TitleText.AddToClassList("grey-text");
            TitleText.AddToClassList("sub-text");
            TitleText.text = "Emptying logs";
            Add(TitleText);

            for (int i = 0; i < 10; i++)
            {
                var child = new TextElement();
                child.name = "EmptyLog";
                child.text = "Robert Sampletext Jr. - 11:56 AM";
                child.AddToClassList("black-text");
                child.AddToClassList("normal-text");
                Add(child);
            }
        }
    }
}