using Commons.Models;
using Commons.Types;
using UI.Base;
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

        // Buttons
        private VisualElement _buttonContainer;
        private AnimatedButton _leftButton;
        private AnimatedButton _rightButton;

        public TaskDetails() : base(nameof(TaskDetails))
        {
            ConfigureUss(nameof(TaskDetails));

            CreateHeader();
            CreatePanels();
            CreateButtons();
        }

        private void CreateHeader()
        {
            _taskDetailsHeader = new TaskDetailsHeader();
            Add(_taskDetailsHeader);
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

        private void CreateButtons()
        {
            _buttonContainer = new VisualElement() { name = "ButtonContainer" };
            Add(_buttonContainer);

            _leftButton = new AnimatedButton("LeftButton");
            _leftButton.AddToTextClassList("white-text");
            _leftButton.AddToTextClassList("normal-text");
            _leftButton.AddToClassList("green-button");
            _leftButton.AddToClassList("rounded-button-32px");
            _buttonContainer.Add(_leftButton);

            _rightButton = new AnimatedButton("RightButton");
            _rightButton.SetText("Reject");
            _rightButton.AddToTextClassList("white-text");
            _rightButton.AddToTextClassList("normal-text");
            _rightButton.AddToClassList("reject");
            _rightButton.AddToClassList("red-button");
            _rightButton.AddToClassList("rounded-button-32px");
            _buttonContainer.Add(_rightButton);
        }

        public void ShowTaskData(TaskData taskData)
        {
            if (taskData.TaskStatus == TaskStatus.InProgress)
            {
                _leftButton.SetText("Complete");
                EnableInClassList("complete", true);
                EnableInClassList("focus", false);
            }
            else
            {
                _leftButton.SetText("Focus");
                EnableInClassList("complete", false);
                EnableInClassList("focus", true);
            }

            _destinationPanel.SetAddressText(taskData.McpData.Address);
        }
    }
}