using UI.Common;
using UI.Views.Tasks.Tasks;

namespace UI.Views.Tasks
{
    public class TasksView : View
    {
        private readonly TaskListSubview _taskListSubview;
        
        public TasksView() : base("Tasks")
        {
            _taskListSubview = new TaskListSubview();
            Add(_taskListSubview);
        }
    }
}