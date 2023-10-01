using UI.Common;

namespace UI.Views.Tasks.Tasks
{
    public class TaskListSubview : Subview
    {
        private readonly TaskList _taskList;

        public TaskListSubview() : base("TaskList")
        {
            _taskList = new TaskList();
            Add(_taskList);
        }
    }
}