﻿using System;
using System.Collections;
using System.Collections.Generic;
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
        public event Action CurrentReceiverReadMessages;
        public Commons.Models.UserProfile OtherUserProfile { get; set; }
        public int CurrentMessageCount { get; set; }

        protected override IEnumerator CreateRequest(Action callback)
        {
            yield return RequestHelper.SendPostRequest<GetMessagesBetweenTwoUsersResponse>(
                Endpoints.Messaging.GetMessagesBetweenTwoUsers,
                new GetMessagesBetweenTwoUsersRequest()
                {
                    UserAccountId = AuthenticationManager.Instance.UserAccountId,
                    OtherUserAccountId = OtherUserProfile.Id,
                    CurrentMessageCount = CurrentMessageCount,
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
            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Messaging.SEND_MESSAGE, (SendMessageBroadcastData data) =>
            {
                Data = new GetMessagesBetweenTwoUsersResponse
                {
                    Messages = data.Messages,
                    IsContinuous = false
                };
                DataStoreManager.Instance.ScheduleOnMainThread(() => OnDataUpdated(Data));
            });

            AuthenticationManager.Instance.HubConnection.On(HubHandlers.Messaging.READ_MESSAGE,
                (ReadAllMessagesBroadcastData data) =>
                {
                    if (data.ReceiverId == OtherUserProfile.Id)
                        DataStoreManager.Instance.ScheduleOnMainThread(() => CurrentReceiverReadMessages?.Invoke());
                });
        }

        public void SendMessage(string content)
        {
            Data.Messages.Clear();
            Data.Messages.Add(new Message
            {
                SenderProfileId = AuthenticationManager.Instance.UserAccountId,
                ReceiverProfileId = OtherUserProfile.Id,
                Content = content,
                Timestamp = DateTime.Now
            });

            Data.IsContinuous = true;
            OnDataUpdated(Data);

            DataStoreManager.Instance.ScheduleOnMainThread(() => DataStoreManager.Messaging.ContactList.UserSentMessages?.Invoke(
                new SendMessageBroadcastData
                {
                    Messages = new List<Message>()
                    {
                        new Message
                        {
                            SenderProfileId = AuthenticationManager.Instance.UserAccountId,
                            ReceiverProfileId = OtherUserProfile.Id,
                            Content = content,
                            Timestamp = DateTime.UtcNow
                        }
                    },
                }));

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