using System;
using System.Linq;
using Authentication;
using Commons.Communications.Mcps;
using Commons.Communications.Tasks;
using Commons.Endpoints;
using Commons.Models;
using Commons.Types;
using Requests;
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

        private TaskData _taskData;

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
            _leftButton.Clicked += () =>
            {
                if (_taskData.TaskStatus == TaskStatus.NotStarted)
                {
                    DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest(Endpoints.TaskData.FocusTask, new FocusTaskRequest()
                        {
                            TaskId = _taskData.Id,
                            WorkerId = AuthenticationManager.Instance.UserAccountId,
                        },
                        success =>
                        {
                            if (success)
                            {
                                DataStoreManager.Tasks.PersonalTaskList.SendRequest(() =>
                                    ShowTaskData(DataStoreManager.Tasks.PersonalTaskList.Data.Tasks.First(task => task.Id == _taskData.Id)));
                            }
                        }));
                }
                else
                {
                    DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest(Endpoints.TaskData.CompleteTask, new FocusTaskRequest()
                        {
                            TaskId = _taskData.Id,
                            WorkerId = AuthenticationManager.Instance.UserAccountId,
                        },
                        success =>
                        {
                            if (success)
                            {
                                DataStoreManager.Tasks.PersonalTaskList.SendRequest(() => GetFirstAncestorOfType<TasksView>().ShowTaskList());
                            }
                        }));
                }
            };

            _rightButton = new AnimatedButton("RightButton");
            _rightButton.SetText("Reject");
            _rightButton.AddToTextClassList("white-text");
            _rightButton.AddToTextClassList("normal-text");
            _rightButton.AddToClassList("reject");
            _rightButton.AddToClassList("red-button");
            _rightButton.AddToClassList("rounded-button-32px");
            _buttonContainer.Add(_rightButton);
            _rightButton.Clicked += () =>
            {
                DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest(Endpoints.TaskData.RejectTask, new FocusTaskRequest()
                    {
                        TaskId = _taskData.Id,
                        WorkerId = AuthenticationManager.Instance.UserAccountId,
                    },
                    success =>
                    {
                        if (success)
                        {
                            DataStoreManager.Tasks.PersonalTaskList.SendRequest(() => GetFirstAncestorOfType<TasksView>().ShowTaskList());
                        }
                    }));
            };
        }

        public void ShowTaskData(TaskData taskData)
        {
            _taskData = taskData;

            if (taskData.TaskStatus == TaskStatus.InProgress)
            {
                _buttonContainer.style.display = DisplayStyle.Flex;
                _leftButton.SetText("Complete");
                EnableInClassList("complete", true);
                EnableInClassList("focus", false);
            }
            else if (taskData.TaskStatus == TaskStatus.NotStarted)
            {
                _buttonContainer.style.display = DisplayStyle.Flex;
                _leftButton.SetText("Focus");
                EnableInClassList("complete", false);
                EnableInClassList("focus", true);
            }
            else
            {
                _buttonContainer.style.display = DisplayStyle.None;
            }

            _destinationPanel.SetAddressText(taskData.McpData.Address);
            _currentLoadPanel.SetCurrentLoadText(DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[_taskData.McpData.Id]);

            DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<GetEmptyRecordsResponse>(Endpoints.McpData.GetEmptyRecords,
                new GetEmptyRecordsRequest
                {
                    McpId = taskData.McpDataId,
                    CountLimit = 50,
                    DateTimeLimit = DateTime.Now.AddDays(-5),
                },
                (success, result) =>
                {
                    if (success)
                    {
                        _emptyingLogPanel.SetEmptyingLogText(result.Results.ToList());
                    }
                }));
        }
    }
}