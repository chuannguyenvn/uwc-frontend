using Commons.Models;
using Commons.Types;
using Requests;
using Settings;
using UI.Base;
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

        public TaskListEntry(TaskData taskData) : base(nameof(TaskListEntry))
        {
            ConfigureUss(nameof(TaskListEntry));

            CreateTimeline();
            CreateCard(taskData);
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

        private void CreateCard(TaskData taskData)
        {
            var fillStatus = McpFillStatusHelper.GetStatus(DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[taskData.McpDataId]);
            switch (taskData.TaskStatus)
            {
                case TaskStatus.InProgress:
                    _content = new FocusedTaskCard(taskData, fillStatus);
                    _statusText.text = "Ongoing";
                    break;
                case TaskStatus.NotStarted:
                    _content = new UnfocusedTaskCard(taskData, fillStatus);
                    _statusText.text = "Pending";
                    break;
                case TaskStatus.Completed:
                case TaskStatus.Rejected:
                    _content = new CompletedTaskCard(taskData);
                    _statusText.text = taskData.LastStatusChangeTimestamp.ToString("hh:mm");
                    break;
            }

            _content.RegisterCallback<MouseUpEvent>(_ => { GetFirstAncestorOfType<TasksView>().ShowTaskDetails(taskData); });

            Add(_content);
        }
    }
}