using System.Collections;
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
            EstablishHubConnection();
        }
        
        protected async void EstablishHubConnection()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://" + Endpoints.DOMAIN + "/hub",
                    options => options.AccessTokenProvider = () => Task.FromResult(AuthenticationManager.Instance.JWT))
                .Build();

            await HubConnection.StartAsync();
        }
        
        protected async void CloseHubConnection()
        {
            await HubConnection.StopAsync();
        }
    }
}