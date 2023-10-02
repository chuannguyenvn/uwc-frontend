using Constants;
using UI.Base;
using UI.Views.Tasks.Details;
using UI.Views.Tasks.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks
{
    public class TasksView : View
    {
        public TaskList TaskList;
        public TaskDetails TaskDetails;

        public TasksView() : base(nameof(TasksView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/TasksView"));
            AddToClassList("full-view");

            TaskList = new TaskList();
            Add(TaskList);
            
            TaskDetails = new TaskDetails();
            Add(TaskDetails);
            
            if (!Configs.IS_DESKTOP) TaskDetails.style.display = DisplayStyle.None;
        }
    }
}