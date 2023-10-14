using Requests.DataStores.Mcps;

namespace Requests
{
    public class DataStoreManager : Singleton<DataStoreManager>
    {
        public class Mcps
        {
            public static ListViewStore ListView { get; } = new();
        }
    }
}