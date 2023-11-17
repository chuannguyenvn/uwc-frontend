using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class EmptyingLogPanel : Panel
    {
        private TextElement _titleText;

        public EmptyingLogPanel() : base(nameof(EmptyingLogPanel))
        {
            ConfigureUss(nameof(EmptyingLogPanel));

            AddToClassList("rounded-32px");

            CreateTitleText();
            CreateLogs();
        }

        private void CreateTitleText()
        {
            _titleText = new TextElement { name = "TitleText" };
            _titleText.AddToClassList("grey-text");
            _titleText.AddToClassList("sub-text");
            _titleText.text = "Emptying logs";
            Add(_titleText);
        }

        private void CreateLogs()
        {
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