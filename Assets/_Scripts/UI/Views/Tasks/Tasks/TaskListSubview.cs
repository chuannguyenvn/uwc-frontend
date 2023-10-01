using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

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

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<TaskListSubview, UxmlTraits>
        {
        }

        #endregion
    }
}