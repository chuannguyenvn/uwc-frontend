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
        private TaskList _taskList;
        private TaskDetails _taskDetails;
        private AssignTaskFlow _assignTaskFlow;
        private VisualElement _assignTaskButton;
        private VisualElement _assignTaskButtonIcon;

        public TasksView() : base(nameof(TasksView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/TasksView"));
            AddToClassList("tasks-view");

            if (Configs.IS_DESKTOP) CreateForDesktop();
            else CreateForMobile();
        }

        private void CreateForDesktop()
        {
            AddToClassList("side-view");

            _taskList = new TaskList();
            Add(_taskList);

            _assignTaskFlow = new AssignTaskFlow();
            _assignTaskFlow.style.display = DisplayStyle.None;
            Add(_assignTaskFlow);

            _assignTaskButton = new Button() { name = "AssignTaskButton" };
            Add(_assignTaskButton);

            _assignTaskButtonIcon = new VisualElement() { name = "AssignTaskButtonIcon" };
            _assignTaskButton.Add(_assignTaskButtonIcon);

            _assignTaskButton.RegisterCallback<ClickEvent>(evt =>
            {
                _assignTaskFlow.style.display = DisplayStyle.Flex;
                _assignTaskButton.style.display = DisplayStyle.None;
                _taskList.style.display = DisplayStyle.None;
            });
        }

        private void CreateForMobile()
        {
            AddToClassList("full-view");

            _taskList = new TaskList();
            Add(_taskList);

            _taskDetails = new TaskDetails();
            _taskDetails.style.display = DisplayStyle.None;
            Add(_taskDetails);
        }

        public void ShowTaskDetails()
        {
            _taskList.style.display = DisplayStyle.None;
            _taskDetails.style.display = DisplayStyle.Flex;
        }

        public void ShowTaskList()
        {
            _taskDetails.style.display = DisplayStyle.None;
            _taskList.style.display = DisplayStyle.Flex;
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