using Commons.Models;
using Commons.Types;
using Newtonsoft.Json;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

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
            _mcpAddressEntry = new PopupInformationEntry("MCP address");
            AddContent(_mcpAddressEntry);

            _assignerEntry = new PopupInformationEntry("Assigned by");
            AddContent(_assignerEntry);

            _assigneeEntry = new PopupInformationEntry("Assigned to");
            AddContent(_assigneeEntry);

            _createdTimestampEntry = new PopupInformationEntry("Created at");
            AddContent(_createdTimestampEntry);

            _completeByTimestampEntry = new PopupInformationEntry("Complete by");
            AddContent(_completeByTimestampEntry);

            _statusEntry = new PopupInformationEntry("Last status");
            AddContent(_statusEntry);

            _lastStatusChangeTimestampEntry = new PopupInformationEntry("Last status change at");
            AddContent(_lastStatusChangeTimestampEntry);
        }

        public override void SetContent(TaskData data)
        {
            Title.text = "Cleaning task";

            _mcpAddressEntry.SetValue(data.McpData.Address);
            _assignerEntry.SetValue(data.AssignerProfile.FirstName + " " + data.AssignerProfile.LastName);
            _assigneeEntry.SetValue(data.AssigneeProfile.FirstName + " " + data.AssigneeProfile.LastName);
            _createdTimestampEntry.SetValue(data.CreatedTimestamp.ToString("hh:mm tt dd/MM"));
            _completeByTimestampEntry.SetValue(data.CompleteByTimestamp.ToString("hh:mm tt dd/MM"));
            _statusEntry.SetValue(data.TaskStatus.GetFriendlyString());
            _lastStatusChangeTimestampEntry.SetValue(data.LastStatusChangeTimestamp.ToString("hh:mm tt dd/MM"));
        }
    }
}