using Authentication;
using Commons.Communications.Status;
using Commons.HubHandlers;
using Requests.DataStores.Base;
using Microsoft.AspNetCore.SignalR.Client;

namespace Requests.DataStores.Implementations.OnlineStatus
{
    public class OnlineStatusStore : ServerSendInBackgroundDataStore<OnlineStatusBroadcastData>
    {
        public OnlineStatusStore()
        {
            AuthenticationManager.Instance.Initialized += data =>
            {
                Data = data.OnlineStatusBroadcastData;
                OnDataUpdated(Data);
            };
        }

        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.OnlineStatus.UPDATE_STATUSES, (OnlineStatusBroadcastData data) =>
            {
                Data = data;
                OnDataUpdated(Data);
            });
        }
    }
}