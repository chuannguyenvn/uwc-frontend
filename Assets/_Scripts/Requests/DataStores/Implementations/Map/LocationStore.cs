using Commons.Communications.Map;
using Commons.HubHandlers;
using Managers;
using Microsoft.AspNetCore.SignalR.Client;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Map
{
    public class LocationStore : ServerSendInBackgroundDataStore<LocationBroadcastData>
    {
        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Location.BROADCAST_LOCATION,
                (LocationBroadcastData data) => { OnDataUpdated(data); });
        }
    }
}