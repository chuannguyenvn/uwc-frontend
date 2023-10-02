using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class TaskDetails : View
    {
        public TaskDetailsHeader TaskDetailsHeader;
        
        public TaskDetails() : base(nameof(TaskDetails))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/TaskDetails"));
            
            TaskDetailsHeader = new TaskDetailsHeader();
            Add(TaskDetailsHeader);
        }
    }
}