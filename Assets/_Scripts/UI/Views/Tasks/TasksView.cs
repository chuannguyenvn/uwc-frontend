using Requests;
using Settings;
using UI.Base;
using UI.Views.Mcps.AssignTaskProcedure;
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
        public AssignTaskFlow AssignTaskFlow;

        public TasksView() : base(nameof(TasksView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/TasksView"));

            if (Configs.IS_DESKTOP)
            {
                AddToClassList("side-view");

                TaskList = new TaskList();
                Add(TaskList);

                // AssignTaskFlow = new AssignTaskFlow();
                // Add(AssignTaskFlow);
            }
            else
            {
                AddToClassList("full-view");

                TaskList = new TaskList();
                Add(TaskList);

                TaskDetails = new TaskDetails();
                TaskDetails.style.display = DisplayStyle.None;
                Add(TaskDetails);
            }
        }

        public override void FocusView()
        {
            if (Configs.IS_DESKTOP) DataStoreManager.Tasks.AllTaskList.Focus();
            else DataStoreManager.Tasks.PersonalTaskList.Focus();
        }

        public override void UnfocusView()
        {
            if (Configs.IS_DESKTOP) DataStoreManager.Tasks.AllTaskList.Unfocus();
            else DataStoreManager.Tasks.PersonalTaskList.Unfocus();
        }
    }
}