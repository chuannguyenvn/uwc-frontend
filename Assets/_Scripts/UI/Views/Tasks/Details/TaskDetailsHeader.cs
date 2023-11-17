using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class TaskDetailsHeader : AdaptiveElement
    {
        private VisualElement _backButton;
        private TextElement _titleText;

        public TaskDetailsHeader() : base(nameof(TaskDetailsHeader))
        {
            ConfigureUss(nameof(TaskDetailsHeader));

            CreateBackButton();
            CreateTitleText();
        }

        private void CreateBackButton()
        {
            _backButton = new VisualElement { name = "BackButton" };
            _backButton.RegisterCallback<ClickEvent>(_ => { GetFirstAncestorOfType<TasksView>().ShowTaskList(); });
            Add(_backButton);
        }

        private void CreateTitleText()
        {
            _titleText = new TextElement { name = "TitleText" };
            _titleText.text = "Task details";
            _titleText.AddToClassList("normal-text");
            _titleText.AddToClassList("white-text");
            Add(_titleText);
        }
    }
}