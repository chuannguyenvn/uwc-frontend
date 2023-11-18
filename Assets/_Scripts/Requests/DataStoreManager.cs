using Requests.DataStores.Implementations.Map;
using Requests.DataStores.Implementations.Mcps;
using Requests.DataStores.Implementations.Messaging;
using Requests.DataStores.Implementations.OnlineStatus;
using Requests.DataStores.Implementations.Reports;
using Requests.DataStores.Implementations.Tasks;
using Utilities;

namespace Requests
{
    public class DataStoreManager : Singleton<DataStoreManager>
    {
        public static class Mcps
        {
            public static ListViewStore ListView { get; } = new();
        }

        public static class Messaging
        {
            public static ContactListStore ContactList { get; } = new();
            public static InboxMessageListStore InboxMessageList { get; } = new();
        }

        public static class Map
        {
            public static WorkerLocationStore WorkerLocation { get; } = new();
            public static McpLocationStore McpLocation { get; } = new();
        }

        public static class Reporting
        {
            public static ReportingViewStore ReportingView { get; } = new();
        }

        public static class OnlineStatus
        {
            public static OnlineStatusStore Status { get; } = new();
        }

        public static class Tasks
        {
            public static AllTaskListStore AllTaskList { get; } = new();
            public static PersonalTaskListStore PersonalTaskList { get; } = new();
        }
    }
}