using Commons.Models;
using Commons.Types;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Tasks.Tasks
{
    public class TaskListEntry : AdaptiveElement
    {
        private VisualElement _timelineContainer;
        private VisualElement _upLine;
        private VisualElement _downLine;
        private TextElement _statusText;

        private VisualElement _content;

        public TaskListEntry(TaskData taskData, TaskType taskType) : base(nameof(TaskListEntry))
        {
            ConfigureUss(nameof(TaskListEntry));

            CreateTimeline();
            CreateCard(taskData, taskType);
        }

        private void CreateTimeline()
        {
            _timelineContainer = new VisualElement { name = "TimelineContainer" };
            Add(_timelineContainer);

            _upLine = new VisualElement { name = "UpLine" };
            _upLine.AddToClassList("timeline-line");
            _timelineContainer.Add(_upLine);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.AddToClassList("sub-text");
            _statusText.AddToClassList("grey-text");
            _timelineContainer.Add(_statusText);

            _downLine = new VisualElement { name = "DownLine" };
            _downLine.AddToClassList("timeline-line");
            _timelineContainer.Add(_downLine);
        }

        private void CreateCard(TaskData taskData, TaskType taskType)
        {
            switch (taskType)
            {
                case TaskType.Focused:
                    _content = new FocusedTaskCard(taskData, Utility.GetRandomEnumValue<McpFillStatus>());
                    _statusText.text = "Ongoing";
                    break;
                case TaskType.Unfocused:
                    _content = new UnfocusedTaskCard(taskData, Utility.GetRandomEnumValue<McpFillStatus>());
                    _statusText.text = "Pending";
                    break;
                case TaskType.Completed:
                    _content = new CompletedTaskCard(taskData);
                    _statusText.text = "9:41AM";
                    break;
            }

            _content.RegisterCallback<MouseUpEvent>(_ => { GetFirstAncestorOfType<TasksView>().ShowTaskDetails(); });

            Add(_content);
        }
    }
}