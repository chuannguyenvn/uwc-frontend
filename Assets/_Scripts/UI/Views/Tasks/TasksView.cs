using Commons.Models;
using Requests;
using Settings;
using UI.Base;
using UI.Views.Mcps.AssignTaskProcedure;
using UI.Views.Tasks.Details;
using UI.Views.Tasks.Tasks;
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

        private TaskDetailsPopup _taskDetailsPopup;

        public TasksView() : base(nameof(TasksView))
        {
            ConfigureUss(nameof(TasksView));

            if (Configs.IS_DESKTOP) CreateForDesktop();
            else CreateForMobile();
        }

        private void CreateForDesktop()
        {
            AddToClassList("side-view");

            _taskList = new TaskList();
            Add(_taskList);

            _assignTaskFlow = new AssignTaskFlow(this);
            _assignTaskFlow.style.display = DisplayStyle.None;
            Add(_assignTaskFlow);

            _assignTaskButton = new Button() { name = "AssignTaskButton" };
            Add(_assignTaskButton);

            _assignTaskButtonIcon = new VisualElement() { name = "AssignTaskButtonIcon" };
            _assignTaskButton.Add(_assignTaskButtonIcon);

            _taskDetailsPopup = new TaskDetailsPopup();
            Root.Instance.AddPopup(_taskDetailsPopup);

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

        public void ShowTaskDetails(TaskData taskData)
        {
            if (Configs.IS_DESKTOP)
            {
                _taskDetailsPopup.SetContent(taskData);
                _taskDetailsPopup.Show();
            }
            else
            {
                _taskList.style.display = DisplayStyle.None;
                _taskDetails.style.display = DisplayStyle.Flex;
            }
        }

        public void ShowTaskList()
        {
            _taskDetails.style.display = DisplayStyle.None;
            _taskList.style.display = DisplayStyle.Flex;
        }

        public override void FocusView()
        {
            if (Configs.IS_DESKTOP)
            {
                DataStoreManager.Tasks.AllTaskList.Focus();
            }
            else DataStoreManager.Tasks.PersonalTaskList.Focus();
        }

        public override void UnfocusView()
        {
            if (Configs.IS_DESKTOP)
            {
                DataStoreManager.Tasks.AllTaskList.Unfocus();
                _assignTaskFlow.Reset();
                BackToListView();
            }
            else DataStoreManager.Tasks.PersonalTaskList.Unfocus();
        }

        public void BackToListView()
        {
            _assignTaskFlow.style.display = DisplayStyle.None;
            _assignTaskButton.style.display = DisplayStyle.Flex;
            _taskList.style.display = DisplayStyle.Flex;
        }
    }
}