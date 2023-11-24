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
        private AnimatedButton _completeButton;
        private AnimatedButton _rejectButton;

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

            _completeButton = new AnimatedButton("Complete");
            _completeButton.SetText("Complete");
            _completeButton.AddToTextClassList("white-text");
            _completeButton.AddToTextClassList("normal-text");
            _completeButton.AddToClassList("green-button");
            _completeButton.AddToClassList("rounded-button-32px");
            _buttonContainer.Add(_completeButton);

            _rejectButton = new AnimatedButton("Reject");
            _rejectButton.SetText("Reject");
            _rejectButton.AddToTextClassList("white-text");
            _rejectButton.AddToTextClassList("normal-text");
            _rejectButton.AddToClassList("red-button");
            _rejectButton.AddToClassList("rounded-button-32px");
            _buttonContainer.Add(_rejectButton);
        }
    }
}