using System;
using System.Collections;
using Authentication;
using Commons.Communications.Messages;
using Commons.Endpoints;
using Commons.HubHandlers;
using Commons.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Requests.DataStores.Base;
using UnityEngine;

namespace Requests.DataStores.Implementations.Messaging
{
    public class InboxMessageListStore : ServerSendInBackgroundDataStore<GetMessagesBetweenTwoUsersResponse>
    {
        public Commons.Models.UserProfile OtherUserProfile { get; set; }

        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendPostRequest<GetMessagesBetweenTwoUsersResponse>(
                Endpoints.Messaging.GetMessagesBetweenTwoUsers,
                new GetMessagesBetweenTwoUsersRequest()
                {
                    UserAccountId = AuthenticationManager.Instance.UserAccountId,
                    OtherUserAccountId = OtherUserProfile.Id,
                },
                (success, response) =>
                {
                    if (success)
                    {
                        OnDataUpdated(response);
                    }
                }
            );
        }

        protected override void EstablishHubConnection()
        {
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Messaging.SEND_MESSAGE, (SendMessageBroadcastData data) =>
            {
                Data.Messages.Add(data.NewMessage);
                OnDataUpdated(Data);
            });
        }

        public void SendMessage(string content)
        {
            Data.Messages.Add(new Message
            {
                SenderProfileId = AuthenticationManager.Instance.UserAccountId,
                ReceiverProfileId = OtherUserProfile.Id,
                Content = content,
                Timestamp = DateTime.Now
            });
            OnDataUpdated(Data);

            SendRequest(RequestHelper.SendPostRequest(
                Endpoints.Messaging.SendMessage,
                new SendMessageRequest()
                {
                    SenderAccountId = AuthenticationManager.Instance.UserAccountId,
                    ReceiverAccountId = OtherUserProfile.Id,
                    Content = content,
                },
                success =>
                {
                    if (!success) Debug.LogError("Failed to send message");
                }
            ));
        }
    }
}