using System;
using System.Collections;
using Authentication;
using Commons.Communications.Messages;
using Commons.Endpoints;
using Commons.HubHandlers;
using Requests.DataStores.Base;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

namespace Requests.DataStores.Implementations.Messaging
{
    public class ContactListStore : ServerSendInBackgroundDataStore<GetPreviewMessagesResponse>
    {
        public event Action<SendMessageBroadcastData> UserSentMessages;
        public event Action<int> UserIdReadMessages;

        protected override IEnumerator CreateRequest(Action callback)
        {
            yield return RequestHelper.SendPostRequest<GetPreviewMessagesResponse>(
                Endpoints.Messaging.GetPreviewMessages,
                new GetPreviewMessagesRequest()
                {
                    UserAccountId = AuthenticationManager.Instance.UserAccountId
                },
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response);
                        callback?.Invoke();
                    }
                }
            );
        }

        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Messaging.SEND_MESSAGE,
                (SendMessageBroadcastData data) =>
                {
                    DataStoreManager.Instance.ScheduleOnMainThread(() => UserSentMessages?.Invoke(data));
                });

            // AuthenticationManager.Instance.HubConnection.On(HubHandlers.Messaging.READ_MESSAGE,
            //     (ReadAllMessagesBroadcastData data) =>
            //     {
            //         DataStoreManager.Instance.ScheduleOnMainThread(() => UserIdReadMessages?.Invoke(data.ReceiverId));
            //     });
        }
    }
}