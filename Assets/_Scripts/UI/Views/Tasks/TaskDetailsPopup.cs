using Commons.Models;
using Commons.Types;
using LocalizationNS;
using UI.Base;

namespace UI.Views.Tasks
{
    public class TaskDetailsPopup : DataBasedFullscreenPopup<TaskData>
    {
        private PopupInformationEntry _mcpAddressEntry;
        private PopupInformationEntry _assignerEntry;
        private PopupInformationEntry _assigneeEntry;
        private PopupInformationEntry _createdTimestampEntry;
        private PopupInformationEntry _completeByTimestampEntry;
        private PopupInformationEntry _lastStatusChangeTimestampEntry;
        private PopupInformationEntry _statusEntry;

        public TaskDetailsPopup()
        {
            ConfigureUss(nameof(TaskDetailsPopup));

            CreateDetails();
        }

        private void CreateDetails()
        {
            _mcpAddressEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.MCP_ADDRESS));
            AddContent(_mcpAddressEntry);

            _assignerEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.ASSIGNED_BY));
            AddContent(_assignerEntry);

            _assigneeEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.ASSIGNED_TO));
            AddContent(_assigneeEntry);

            _createdTimestampEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.CREATED_AT));
            AddContent(_createdTimestampEntry);

            _completeByTimestampEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.COMPLETE_BY));
            AddContent(_completeByTimestampEntry);

            _statusEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.LAST_STATUS));
            AddContent(_statusEntry);

            _lastStatusChangeTimestampEntry = new PopupInformationEntry(Localization.GetSentence(Sentence.TasksView.LAST_STATUS_CHANGE_AT));
            AddContent(_lastStatusChangeTimestampEntry);
        }

        public override void SetContent(TaskData data)
        {
            Title.text = Localization.GetSentence(Sentence.TasksView.CLEANING_TASK);

            _mcpAddressEntry.SetValue(data.McpData.Address);
            _assignerEntry.SetValue(data.AssignerProfile.FirstName + " " + data.AssignerProfile.LastName);
            _assigneeEntry.SetValue(data.AssigneeProfile?.FirstName + " " + data.AssigneeProfile?.LastName);
            _createdTimestampEntry.SetValue(data.CreatedTimestamp.ToString("hh:mmtt dd/MM/yy"));
            _completeByTimestampEntry.SetValue(data.CompleteByTimestamp.ToString("hh:mmtt dd/MM/yy"));
            _statusEntry.SetValue(data.TaskStatus.GetFriendlyString());
            _lastStatusChangeTimestampEntry.SetValue(data.LastStatusChangeTimestamp.ToString("hh:mmtt dd/MM/yy"));
        }
    }
}