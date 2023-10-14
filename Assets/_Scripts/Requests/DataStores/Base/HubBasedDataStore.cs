using System.Threading.Tasks;
using Managers;
using Microsoft.AspNetCore.SignalR.Client;

namespace Requests.DataStores.Base
{
    public abstract class HubBasedDataStore<T> : DataStore<T>
    {
        public HubConnection HubConnection { get; private set; }

        public HubBasedDataStore()
        {
        }
        
        ~HubBasedDataStore()
        {
            CloseHubConnection();
        }

        protected virtual async void EstablishHubConnection()
        {

        }

        protected virtual async void CloseHubConnection()
        {
        }
    }
}