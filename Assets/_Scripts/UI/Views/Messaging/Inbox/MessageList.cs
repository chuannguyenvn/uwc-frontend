using System;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Commons.Communications.Messages;
using Commons.Endpoints;
using Commons.Models;
using Newtonsoft.Json;
using Requests;
using UI.Base;
using UI.Reusables;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Views.Messaging.Inbox
{
    public class MessageList : AdaptiveElement
    {
        private ScrollViewWithShadow _scrollView;
        private List<MessageListEntry> _messageListEntries = new List<MessageListEntry>();

        public MessageList() : base(nameof(MessageList))
        {
            ConfigureUss(nameof(MessageList));

            CreateScrollView();

            DataStoreManager.Messaging.InboxMessageList.DataUpdated += DataUpdatedHandler;
            DataStoreManager.Messaging.InboxMessageList.CurrentReceiverReadMessages += MarkAllMessagesAsRead;
        }

        ~MessageList()
        {
            DataStoreManager.Messaging.InboxMessageList.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(GetMessagesBetweenTwoUsersResponse data)
        {
            Debug.Log("update");
            var isJustSentMessage = data.Messages.Count == 1 && data.Messages[0].Timestamp > DateTime.Now.AddSeconds(-5);

            if (data.IsContinuous)
            {
                _scrollView.MarkOldVerticalScrollerValue();
            }
            else if (!isJustSentMessage)
            {
                Debug.Log("clear");
                ClearMessages();
            }

            data.Messages = data.Messages.DistinctBy(message => message.Id).ToList();
            data.Messages.Sort((a, b) => b.Timestamp.CompareTo(a.Timestamp));

            Debug.Log(JsonConvert.SerializeObject(data.Messages, Formatting.Indented));

            var messageListEntries = new List<MessageListEntry>();
            foreach (var message in data.Messages)
            {
                var messageListEntry = new MessageListEntry(message);
                _scrollView.AddToScrollView(messageListEntry, isJustSentMessage);
                messageListEntries.Add(messageListEntry);
            }

            _messageListEntries.AddRange(messageListEntries.AsEnumerable().Reverse());

            if (data.IsContinuous && !isJustSentMessage)
            {
                Debug.Log("ScrollToOldVerticalScrollerValue");
                _scrollView.ScrollToOldVerticalScrollerValue();
            }
            else
            {
                Debug.Log("ScrollToLast");
                _scrollView.ScrollToLast();
            }
            
            DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest(Endpoints.Messaging.ReadMessage, new ReadAllMessagesRequest
            {
                SenderId = DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.Id,
                ReceiverId = AuthenticationManager.Instance.UserAccountId,
            }));
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop, scroller =>
            {
                if (Math.Abs(scroller.verticalScroller.value - scroller.verticalScroller.lowValue) > 10f) return;

                DataStoreManager.Messaging.InboxMessageList.CurrentMessageCount = _messageListEntries.Count;
                DataStoreManager.Messaging.InboxMessageList.SendRequest();
            })
            {
                name = "ScrollView"
            };

            Add(_scrollView);

            Root.Instance.ViewUnfocused += (_) => _scrollView.ResetOldVerticalScrollerValue();
        }

        public void ClearMessages()
        {
            _scrollView.Clear();
            _messageListEntries.Clear();
        }

        private void MarkAllMessagesAsRead()
        {
            schedule.Execute(() =>
            {
                foreach (var messageListEntry in _messageListEntries)
                {
                    messageListEntry.MarkAsRead();
                }
            });
        }
    }
}