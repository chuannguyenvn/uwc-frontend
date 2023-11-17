using Commons.Communications.Tasks;
using Commons.Models;
using Requests;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Tasks.Tasks
{
    public class TaskList : View
    {
        public ScrollView ScrollView;

        public TaskList() : base(nameof(TaskList))
        {
            ConfigureUss(nameof(TaskList));

            CreateScrollView();

            if (Configs.IS_DESKTOP) DataStoreManager.Tasks.AllTaskList.DataUpdated += AllTaskListDataUpdatedHandler;
            else DataStoreManager.Tasks.PersonalTaskList.DataUpdated += PersonalTaskListDataUpdatedHandler;

            // ScrollToImmediate(focusedTask);
        }

        ~TaskList()
        {
            if (Configs.IS_DESKTOP) DataStoreManager.Tasks.AllTaskList.DataUpdated -= AllTaskListDataUpdatedHandler;
            else DataStoreManager.Tasks.PersonalTaskList.DataUpdated -= PersonalTaskListDataUpdatedHandler;
        }

        private void CreateScrollView()
        {
            ScrollView = new ScrollView();
            ScrollView.AddToClassList("list-view");
            Add(ScrollView);
        }

        private void AllTaskListDataUpdatedHandler(GetAllTasksResponse getAllTasksResponse)
        {
            ScrollView.Clear();

            foreach (var task in getAllTasksResponse.Tasks)
            {
                task.McpData.Address = Utility.RemoveDiacritics(task.McpData.Address);
                ScrollView.Add(new TaskListEntry(task, TaskType.Unfocused));
            }
        }

        private void PersonalTaskListDataUpdatedHandler(GetTasksOfWorkerResponse getTasksOfWorkerResponse)
        {
            ScrollView.Clear();

            foreach (var task in getTasksOfWorkerResponse.Tasks)
            {
                task.McpData.Address = Utility.RemoveDiacritics(task.McpData.Address);
                ScrollView.Add(new TaskListEntry(task, TaskType.Unfocused));
            }
        }

        private void ScrollToImmediate(VisualElement item)
        {
            int remainingIterations = 4;

            void TryScroll()
            {
                //if both layout and item have a size, then we can scroll immediate
                //otherwise, we need to listen to layout changes then scrollTo

                if (item.layout.width > 0 && ScrollView.layout.width > 0)
                {
                    ScrollView.ScrollTo(item);
                    return;
                }
                else if (remainingIterations <= 0)
                {
                    Debug.LogWarning("Too many layout iterations");

                    ScrollView.ScrollTo(item);
                    return;
                }

                if (ScrollView.layout.width > 0)
                {
                    item.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, item);
                }
                else
                {
                    ScrollView.RegisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged, ScrollView);
                }
            }

            void OnGeometryChanged(GeometryChangedEvent evt, VisualElement target)
            {
                target.UnregisterCallback<GeometryChangedEvent, VisualElement>(OnGeometryChanged);

                //try scrolling after we received a geometry changed event from either the item or scrollView
                //the geometry still might be 0, so keep trying if so

                remainingIterations--;

                TryScroll();
            }

            TryScroll();
        }
    }
}