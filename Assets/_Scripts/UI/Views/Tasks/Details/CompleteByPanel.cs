using System;
using LocalizationNS;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class CompleteByPanel: Panel
    {
        private VisualElement _completeByContainer;
        private TextElement _completeByTitle;
        private TextElement _completeByText;
        
        public CompleteByPanel() : base(nameof(CompleteByPanel))
        {
            ConfigureUss(nameof(CompleteByPanel));

            AddToClassList("rounded-32px");

            CreateCompleteBy();
        }

        private void CreateCompleteBy()
        {
            _completeByContainer = new VisualElement { name = "NumericalLoadContainer" };
            Add(_completeByContainer);

            _completeByTitle = new TextElement { name = "NumericalLoadTitle" };
            _completeByTitle.AddToClassList("sub-text");
            _completeByTitle.AddToClassList("grey-text");
            _completeByTitle.text = Localization.GetSentence(Sentence.TasksView.COMPLETE_BY);
            _completeByContainer.Add(_completeByTitle);

            _completeByText = new TextElement { name = "NumericalLoadText" };
            _completeByText.AddToClassList("normal-text");
            _completeByText.AddToClassList("black-text");
            _completeByText.text = "0.00";
            _completeByContainer.Add(_completeByText);
        }
        
        public void SetCompleteByText(DateTime completeBy)
        {
            _completeByText.text =  completeBy.ToString("hh:mmtt dd/MM");
        }
    }
}