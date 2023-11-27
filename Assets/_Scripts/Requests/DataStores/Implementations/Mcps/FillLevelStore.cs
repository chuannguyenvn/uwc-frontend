using Authentication;
using Commons.Communications.Mcps;
using Commons.HubHandlers;
using Requests.DataStores.Base;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using UnityEngine;

namespace Requests.DataStores.Implementations.Mcps
{
    public class FillLevelStore : ServerSendInBackgroundDataStore<McpFillLevelBroadcastData>
    {
        public FillLevelStore()
        {
            AuthenticationManager.Instance.Initialized += data =>
            {
                Data = data.McpFillLevelBroadcastData;
                OnDataUpdated(Data);
            };
        }

        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.McpFillLevel.BROADCAST_FILL_LEVEL, (McpFillLevelBroadcastData data) =>
            {
                Data = data;
                OnDataUpdated(Data);
            });
        }
    }
}