using Requests.DataStores.Implementations.Mcps;
using Requests.DataStores.Implementations.Messaging;

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
    }
}