using Commons.Communications.Map;
using Commons.HubHandlers;
using Managers;
using Microsoft.AspNetCore.SignalR.Client;
using Requests.DataStores.Base;

namespace Requests.DataStores.Implementations.Map
{
    public class WorkerLocationStore : ServerSendInBackgroundDataStore<WorkerLocationBroadcastData>
    {
        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.WorkerLocation.BROADCAST_LOCATION,
                (WorkerLocationBroadcastData data) => { OnDataUpdated(data); });
        }
    }
}