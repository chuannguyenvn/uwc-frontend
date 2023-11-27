using System;
using Requests.DataStores.Implementations.Map;
using Requests.DataStores.Implementations.Mcps;
using Requests.DataStores.Implementations.Messaging;
using Requests.DataStores.Implementations.OnlineStatus;
using Requests.DataStores.Implementations.Reports;
using Requests.DataStores.Implementations.Setting;
using Requests.DataStores.Implementations.Tasks;
using Requests.DataStores.Implementations.UserProfile;
using Requests.DataStores.Implementations.Vehicles;
using Utilities;

namespace Requests
{
    public class DataStoreManager : Singleton<DataStoreManager>
    {
        public static class Mcps
        {
            public static ListViewStore ListView { get; set; }
            public static FillLevelStore FillLevel { get; set; }
        }

        public static class Messaging
        {
            public static ContactListStore ContactList { get; set; }
            public static InboxMessageListStore InboxMessageList { get; set; }
        }

        public static class Map
        {
            public static WorkerLocationStore WorkerLocation { get; set; }
            public static McpLocationStore McpLocation { get; set; }
        }

        public static class Reporting
        {
            public static ReportingViewStore ReportingView { get; set; }
        }

        public static class OnlineStatus
        {
            public static OnlineStatusStore Status { get; set; }
        }

        public static class UserProfile
        {
            public static AllWorkerProfileListStore AllWorkerProfileList { get; set; }
        }

        public static class Tasks
        {
            public static AllTaskListStore AllTaskList { get; set; }
            public static PersonalTaskListStore PersonalTaskList { get; set; }
        }

        public static class Vehicles
        {
            public static AllVehicleListStore AllVehicleList { get; set; }
        }

        public static class Setting
        {
            public static SettingStore Settings { get; set; }
        }

        private void Start()
        {
            Mcps.ListView = new ListViewStore();
            Mcps.FillLevel = new FillLevelStore();

            Messaging.ContactList = new ContactListStore();
            Messaging.InboxMessageList = new InboxMessageListStore();

            Map.WorkerLocation = new WorkerLocationStore();
            Map.McpLocation = new McpLocationStore();

            Reporting.ReportingView = new ReportingViewStore();

            OnlineStatus.Status = new OnlineStatusStore();

            UserProfile.AllWorkerProfileList = new AllWorkerProfileListStore();

            Tasks.AllTaskList = new AllTaskListStore();
            Tasks.PersonalTaskList = new PersonalTaskListStore();

            Vehicles.AllVehicleList = new AllVehicleListStore();

            Setting.Settings = new SettingStore();
        }
    }
}