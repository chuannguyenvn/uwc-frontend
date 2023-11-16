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
        public VisualElement AssignTaskButton;
        public VisualElement AssignTaskButtonIcon;

        public TasksView() : base(nameof(TasksView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/TasksView"));
            AddToClassList("tasks-view");

            if (Configs.IS_DESKTOP)
            {
                AddToClassList("side-view");

                TaskList = new TaskList();
                Add(TaskList);

                AssignTaskFlow = new AssignTaskFlow();
                AssignTaskFlow.style.display = DisplayStyle.None;
                Add(AssignTaskFlow);

                AssignTaskButton = new Button() { name = "AssignTaskButton" };
                Add(AssignTaskButton);

                AssignTaskButtonIcon = new VisualElement() { name = "AssignTaskButtonIcon" };
                AssignTaskButton.Add(AssignTaskButtonIcon);

                AssignTaskButton.RegisterCallback<ClickEvent>(evt => AssignTaskFlow.style.display = DisplayStyle.Flex);
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

        public new class UxmlFactory : UxmlFactory<TasksView, UxmlTraits>
        {
        }
    }
}