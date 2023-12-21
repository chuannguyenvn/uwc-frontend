using System.Collections.Generic;
using Commons.Models;
using LocalizationNS;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class EmptyingLogPanel : Panel
    {
        private TextElement _titleText;
        private List<TextElement> _emptyingLogTexts = new List<TextElement>();

        public EmptyingLogPanel() : base(nameof(EmptyingLogPanel))
        {
            ConfigureUss(nameof(EmptyingLogPanel));

            AddToClassList("rounded-32px");

            CreateTitleText();
        }

        private void CreateTitleText()
        {
            _titleText = new TextElement { name = "TitleText" };
            _titleText.AddToClassList("grey-text");
            _titleText.AddToClassList("sub-text");
            _titleText.text = Localization.GetSentence(Sentence.TasksView.EMPTYING_LOGS);
            Add(_titleText);
        }

        public void SetEmptyingLogText(List<McpEmptyRecord> records)
        {
            foreach (var textElement in _emptyingLogTexts)
            {
                Remove(textElement);
            }

            _emptyingLogTexts.Clear();

            foreach (var record in records)
            {
                var child = new TextElement();
                child.name = "EmptyLog";
                child.text = record.EmptyingWorker.FirstName + " " + record.EmptyingWorker.LastName + " - " +
                             record.Timestamp.ToString("HH:mmtt dd/MM");
                child.AddToClassList("black-text");
                child.AddToClassList("normal-text");
                Add(child);
                _emptyingLogTexts.Add(child);
            }
        }
    }
}