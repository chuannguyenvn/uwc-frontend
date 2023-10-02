using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class TaskDetails : View
    {
        public TaskDetailsHeader TaskDetailsHeader;
        public PanelList PanelList;
        
        public TaskDetails() : base(nameof(TaskDetails))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/TaskDetails"));
            
            TaskDetailsHeader = new TaskDetailsHeader();
            Add(TaskDetailsHeader);
            
            PanelList = new PanelList();
            Add(PanelList);
            
            PanelList.Add(new DestinationPanel());
            PanelList.Add(new CurrentLoadPanel());
            PanelList.Add(new EmptyingLogPanel());
        }
    }
}