using Commons.Communications.Map;
using Commons.HubHandlers;
using Managers;
using Requests.DataStores.Base;
using Microsoft.AspNetCore.SignalR.Client;

namespace Requests.DataStores.Implementations.Map
{
    public class McpLocationStore : ServerSendInBackgroundDataStore<McpLocationBroadcastData>
    {
        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.McpLocation.BROADCAST_LOCATION,
                (McpLocationBroadcastData data) => { OnDataUpdated(data); });
        }
    }
}