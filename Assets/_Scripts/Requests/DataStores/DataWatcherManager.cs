using Requests.DataStores.Mcps;

namespace Requests.DataStores
{
    public class DataWatcherManager : Singleton<DataWatcherManager>
    {
        public static class Mcps
        {
            public static ListViewWatcher ListView = new();
        }
    }
}