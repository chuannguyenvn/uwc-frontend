using System;
using System.Collections;
using Commons.Communications.Messages;
using Commons.HubHandlers;
using Commons.Models;
using Managers;
using Microsoft.AspNetCore.SignalR.Client;
using Requests.DataStores.Base;
using UnityEngine;

namespace Requests.DataStores.Implementations.Messaging
{
    public class InboxMessageListStore : ServerSendInBackgroundDataStore<GetMessagesBetweenTwoUsersResponse>
    {
        public int OtherUserAccountId { get; set; }

        protected override IEnumerator CreateRequest()
        {
            yield return RequestHelper.SendPostRequest<GetMessagesBetweenTwoUsersResponse>(
                Endpoints.Messaging.GetMessagesBetweenTwoUsers,
                new GetMessagesBetweenTwoUsersRequest()
                {
                    UserAccountId = AuthenticationManager.Instance.UserAccountId,
                    OtherUserAccountId = OtherUserAccountId,
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

        protected override void ListenToHub()
        {
            HubConnection.On(HubHandlers.Messaging.SEND_MESSAGE, (SendMessageBroadcastData data) =>
            {
                Data.Messages.Add(data.NewMessage);
                OnDataUpdated(Data);
            });
        }

        public void SendMessage(string content)
        {
            Data.Messages.Add(new Message
            {
                SenderAccountId = AuthenticationManager.Instance.UserAccountId,
                ReceiverAccountId = OtherUserAccountId,
                Content = content,
                Timestamp = DateTime.Now
            });
            OnDataUpdated(Data);

            SendRequest(RequestHelper.SendPostRequest(
                Endpoints.Messaging.SendMessage,
                new SendMessageRequest()
                {
                    SenderAccountId = AuthenticationManager.Instance.UserAccountId,
                    ReceiverAccountId = OtherUserAccountId,
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