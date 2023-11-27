using Authentication;
using Commons.Communications.Map;
using Commons.HubHandlers;
using Requests.DataStores.Base;
using Microsoft.AspNetCore.SignalR.Client;

namespace Requests.DataStores.Implementations.Map
{
    public class McpLocationStore : ServerSendInBackgroundDataStore<McpLocationBroadcastData>
    {
        public McpLocationStore()
        {
            AuthenticationManager.Instance.Initialized += data =>
            {
                Data = data.McpLocationBroadcastData;
                OnDataUpdated(Data);
            };
        }

        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.McpLocation.BROADCAST_LOCATION,
                (McpLocationBroadcastData data) => { OnDataUpdated(data); });
        }

        protected override void CloseHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.Remove(HubHandlers.McpLocation.BROADCAST_LOCATION);
        }
    }
}