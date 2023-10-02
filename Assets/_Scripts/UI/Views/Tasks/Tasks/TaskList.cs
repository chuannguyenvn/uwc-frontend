using Commons.Models;
using Constants;
using UI.Base;
using UI.Views.Messaging.Contacts;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class TaskList : View
    {
        private ScrollView _scrollView;

        public TaskList() : base(nameof(TaskList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/TaskList"));

            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

            for (int i = 0; i < 30; i++)
            {
                _scrollView.Add(new TaskListEntry(new TaskData()));
            }
        }
    }
}