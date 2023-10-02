using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class TaskDetailsHeader : AdaptiveElement
    {
        public VisualElement BackButton;
        public TextElement TitleText;

        public TaskDetailsHeader() : base(nameof(TaskDetailsHeader))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/TaskDetailsHeader"));

            BackButton = new VisualElement { name = "BackButton" };
            BackButton.RegisterCallback<MouseUpEvent>(_ =>
            {
                RegisterCallback<MouseUpEvent>(_ =>
                {
                    GetFirstAncestorOfType<TasksView>().TaskList.style.display = DisplayStyle.Flex;
                    GetFirstAncestorOfType<TasksView>().TaskDetails.style.display = DisplayStyle.None;
                });
            });
            Add(BackButton);

            TitleText = new TextElement { name = "TitleText" };
            TitleText.text = "Task details";
            TitleText.AddToClassList("normal-text");
            TitleText.AddToClassList("white-text");
            Add(TitleText);
        }
    }
}