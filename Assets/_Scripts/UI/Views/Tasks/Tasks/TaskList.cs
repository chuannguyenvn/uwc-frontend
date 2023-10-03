using Commons.Models;
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

            for (int i = 0; i < 30; i++)
            {
                ScrollView.Add(new TaskListEntry(new TaskData()));
            }
        }
    }
}