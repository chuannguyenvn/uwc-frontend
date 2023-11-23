using System.Collections.Generic;
using Commons.Communications.Tasks;
using LocalizationNS;
using Requests;
using Settings;
using UI.Base;
using UI.Reusables;
using UI.Reusables.Control;
using UI.Reusables.Control.Sort;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Tasks.Tasks
{
    public class TaskList : View
    {
        private ListControl _listControl;
        private ScrollViewWithShadow _scrollView;

        private List<TaskListEntry> _taskListEntries = new List<TaskListEntry>();

        public TaskList() : base(nameof(TaskList))
        {
            ConfigureUss(nameof(TaskList));

            CreateControls();
            CreateScrollView();

            if (Configs.IS_DESKTOP) DataStoreManager.Tasks.AllTaskList.DataUpdated += AllTaskListDataUpdatedHandler;
            else DataStoreManager.Tasks.PersonalTaskList.DataUpdated += PersonalTaskListDataUpdatedHandler;

            // ScrollToImmediate(focusedTask);
        }

        ~TaskList()
        {
            if (Configs.IS_DESKTOP) DataStoreManager.Tasks.AllTaskList.DataUpdated -= AllTaskListDataUpdatedHandler;
            else DataStoreManager.Tasks.PersonalTaskList.DataUpdated -= PersonalTaskListDataUpdatedHandler;
        }

        private void CreateControls()
        {
            _listControl = new ListControl(SearchHandler);
            Add(_listControl);

            if (Configs.IS_DESKTOP)
            {
                _listControl.CreateSortButton(Localization.GetSentence(Sentence.TasksView.MCP_FILL_LEVEL),
                    () => AllTaskListDataUpdatedHandler(DataStoreManager.Tasks.AllTaskList.Data));
                _listControl.CreateSortButton(Localization.GetSentence(Sentence.TasksView.TASK_STATUS),
                    () => AllTaskListDataUpdatedHandler(DataStoreManager.Tasks.AllTaskList.Data));
                _listControl.CreateSortButton(Localization.GetSentence(Sentence.TasksView.CREATED_TIME),
                    () => AllTaskListDataUpdatedHandler(DataStoreManager.Tasks.AllTaskList.Data));
                _listControl.CreateSortButton(Localization.GetSentence(Sentence.TasksView.COMPLETE_BY),
                    () => AllTaskListDataUpdatedHandler(DataStoreManager.Tasks.AllTaskList.Data));
                _listControl.CreateSortButton(Localization.GetSentence(Sentence.TasksView.LAST_CHANGED_TIME),
                    () => AllTaskListDataUpdatedHandler(DataStoreManager.Tasks.AllTaskList.Data));
            }
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop) { name = "ScrollView" };
            Add(_scrollView);
        }

        private void AllTaskListDataUpdatedHandler(GetAllTasksResponse getAllTasksResponse)
        {
            _scrollView.Clear();
            _taskListEntries.Clear();
            foreach (var task in getAllTasksResponse.Tasks)
            {
                task.McpData.Address = task.McpData.Address;
                var newTask = new TaskListEntry(task);
                _taskListEntries.Add(newTask);
            }

            if (_listControl.SortStates[0] == SortType.Ascending)
                _taskListEntries.Sort((a, b) =>
                    DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[a.TaskData.McpData.Id]
                        .CompareTo(DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[b.TaskData.McpData.Id]));
            else if (_listControl.SortStates[0] == SortType.Descending)
                _taskListEntries.Sort((a, b) =>
                    DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[b.TaskData.McpData.Id]
                        .CompareTo(DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[a.TaskData.McpData.Id]));

            if (_listControl.SortStates[1] == SortType.Ascending)
                _taskListEntries.Sort((a, b) => a.TaskData.TaskStatus.CompareTo(b.TaskData.TaskStatus));
            else if (_listControl.SortStates[1] == SortType.Descending)
                _taskListEntries.Sort((a, b) => b.TaskData.TaskStatus.CompareTo(a.TaskData.TaskStatus));

            if (_listControl.SortStates[2] == SortType.Ascending)
                _taskListEntries.Sort((a, b) => a.TaskData.CreatedTimestamp.CompareTo(b.TaskData.CreatedTimestamp));
            else if (_listControl.SortStates[2] == SortType.Descending)
                _taskListEntries.Sort((a, b) => b.TaskData.CreatedTimestamp.CompareTo(a.TaskData.CreatedTimestamp));

            if (_listControl.SortStates[3] == SortType.Ascending)
                _taskListEntries.Sort((a, b) => a.TaskData.CompleteByTimestamp.CompareTo(b.TaskData.CompleteByTimestamp));
            else if (_listControl.SortStates[3] == SortType.Descending)
                _taskListEntries.Sort((a, b) => b.TaskData.CompleteByTimestamp.CompareTo(a.TaskData.CompleteByTimestamp));

            if (_listControl.SortStates[4] == SortType.Ascending)
                _taskListEntries.Sort((a, b) => a.TaskData.LastStatusChangeTimestamp.CompareTo(b.TaskData.LastStatusChangeTimestamp));
            else if (_listControl.SortStates[4] == SortType.Descending)
                _taskListEntries.Sort((a, b) => b.TaskData.LastStatusChangeTimestamp.CompareTo(a.TaskData.LastStatusChangeTimestamp));

            foreach (var mcpEntry in _taskListEntries) _scrollView.AddToScrollView(mcpEntry);
        }

        private void PersonalTaskListDataUpdatedHandler(GetTasksOfWorkerResponse getTasksOfWorkerResponse)
        {
            _scrollView.Clear();
            foreach (var task in getTasksOfWorkerResponse.Tasks)
            {
                task.McpData.Address = task.McpData.Address;
                _scrollView.Add(new TaskListEntry(task));
            }
        }

        private void SearchHandler(string text)
        {
            text = Utility.CreateSearchString(text);
            foreach (var entry in _taskListEntries)
            {
                if (Utility.CreateSearchString(entry.TaskData.McpData.Address, entry.TaskData.AssigneeProfile.FirstName,
                            entry.TaskData.AssigneeProfile.LastName, entry.TaskData.AssignerProfile.FirstName,
                            entry.TaskData.AssignerProfile.LastName)
                        .Contains(text) || text == "")
                {
                    entry.style.display = DisplayStyle.Flex;
                }
                else
                {
                    entry.style.display = DisplayStyle.None;
                }
            }
        }
    }
}