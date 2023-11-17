using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class TaskDetails : View
    {
        // Header
        private TaskDetailsHeader _taskDetailsHeader;

        // Panels
        private PanelList _panelList;
        private DestinationPanel _destinationPanel;
        private CurrentLoadPanel _currentLoadPanel;
        private EmptyingLogPanel _emptyingLogPanel;

        public TaskDetails() : base(nameof(TaskDetails))
        {
            ConfigureUss(nameof(TaskDetails));

            CreateHeader();
            CreatePanels();
        }

        private void CreatePanels()
        {
            _panelList = new PanelList();
            Add(_panelList);

            _destinationPanel = new DestinationPanel();
            _panelList.AddPanel(_destinationPanel);

            _currentLoadPanel = new CurrentLoadPanel();
            _panelList.AddPanel(_currentLoadPanel);

            _emptyingLogPanel = new EmptyingLogPanel();
            _panelList.AddPanel(_emptyingLogPanel);
        }

        private void CreateHeader()
        {
            _taskDetailsHeader = new TaskDetailsHeader();
            Add(_taskDetailsHeader);
        }
    }
}