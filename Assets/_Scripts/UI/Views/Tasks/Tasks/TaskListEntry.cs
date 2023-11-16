﻿using Commons.Models;
using Commons.Types;
using Settings;
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

        public TaskListEntry(TaskData taskData, TaskType taskType) : base(nameof(TaskListEntry))
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
            TimelineContainer.Add(StatusText);

            DownLine = new VisualElement { name = "DownLine" };
            DownLine.AddToClassList("timeline-line");
            TimelineContainer.Add(DownLine);

            switch (taskType)
            {
                case TaskType.Focused:
                    Content = new FocusedTaskCard(taskData, Utility.GetRandomEnumValue<McpFillStatus>());
                    StatusText.text = "Ongoing";
                    break;
                case TaskType.Unfocused:
                    Content = new UnfocusedTaskCard(taskData, Utility.GetRandomEnumValue<McpFillStatus>());
                    StatusText.text = "Pending";
                    break;
                case TaskType.Completed:
                    Content = new CompletedCard(taskData);
                    StatusText.text = "9:41AM";
                    break;
            }

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