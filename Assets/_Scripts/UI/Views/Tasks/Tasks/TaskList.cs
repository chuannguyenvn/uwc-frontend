using Commons.Models;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class TaskList : View
    {
        public ScrollView ScrollView;

        public TaskList() : base(nameof(TaskList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/TaskList"));

            ScrollView = new ScrollView();
            ScrollView.AddToClassList("list-view");
            Add(ScrollView);

            TaskListEntry focusedTask = null;
            for (int i = 0; i < 30; i++)
            {
                var taskType = TaskType.Completed;
                if (i == 15) taskType = TaskType.Focused;
                if (i > 15) taskType = TaskType.Unfocused;

                var newTask = new TaskListEntry(new TaskData(), taskType);
                ScrollView.Add(newTask);

                if (taskType == TaskType.Focused) focusedTask = newTask;
            }

            ScrollToImmediate(focusedTask);
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