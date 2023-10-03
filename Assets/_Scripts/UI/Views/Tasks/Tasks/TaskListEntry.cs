using Commons.Models;
using Commons.Types;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class TaskListEntry : AdaptiveElement
    {
        public VisualElement TimelineContainer;
        public VisualElement UpLine;
        public VisualElement DownLine;
        public TextElement StatusText;

        public VisualElement Content;

        public TaskListEntry(TaskData taskData) : base(nameof(TaskListEntry))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/TaskListEntry"));
            AddToClassList("tasks-list-entry");

            TimelineContainer = new VisualElement { name = "TimelineContainer" };
            Add(TimelineContainer);

            UpLine = new VisualElement { name = "UpLine" };
            UpLine.AddToClassList("timeline-line");
            TimelineContainer.Add(UpLine);

            StatusText = new TextElement { name = "StatusText" };
            StatusText.AddToClassList("sub-text");
            StatusText.AddToClassList("grey-text");
            StatusText.text = "Status";
            TimelineContainer.Add(StatusText);

            DownLine = new VisualElement { name = "DownLine" };
            DownLine.AddToClassList("timeline-line");
            TimelineContainer.Add(DownLine);

            var random = Random.Range(0, 3);
            Content = random switch
            {
                0 => new FocusedTaskCard(Utility.GetRandomEnumValue<McpFillStatus>()),
                1 => new UnfocusedTaskCard(Utility.GetRandomEnumValue<McpFillStatus>()),
                2 => new CompletedCard(),
                _ => Content
            };
            Content.RegisterCallback<MouseUpEvent>(_ =>
            {
                RegisterCallback<MouseUpEvent>(_ =>
                {
                    GetFirstAncestorOfType<TasksView>().TaskList.style.display = DisplayStyle.None;
                    GetFirstAncestorOfType<TasksView>().TaskDetails.style.display = DisplayStyle.Flex;
                });
            });
            Add(Content);
        }
    }
}