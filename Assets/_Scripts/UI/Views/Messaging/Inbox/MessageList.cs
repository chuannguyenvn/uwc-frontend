using System;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Commons.Communications.Messages;
using Commons.Endpoints;
using Commons.Models;
using Requests;
using UI.Base;
using UI.Reusables;
using UnityEngine;
using UnityEngine.UIElements;

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
            if (data.IsContinuous)
            {
                _scrollView.MarkOldVerticalScrollerValue();
            }
            else
            {
                ClearMessages();
            }

            data.Messages.Sort((a, b) => b.Timestamp.CompareTo(a.Timestamp));

            var messageListEntries = new List<MessageListEntry>();
            foreach (var message in data.Messages)
            {
                var messageListEntry = new MessageListEntry(message);
                _scrollView.AddToScrollView(messageListEntry);
                messageListEntries.Add(messageListEntry);
            }
            
            _messageListEntries.AddRange(messageListEntries.AsEnumerable().Reverse());

            if (data.IsContinuous)
            {
                _scrollView.ScrollToOldVerticalScrollerValue();
            }
            else
            {
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
                Debug.Log(scroller.verticalScroller.value - scroller.verticalScroller.lowValue);
                if (Math.Abs(scroller.verticalScroller.value - scroller.verticalScroller.lowValue) > 10f) return;

                DataStoreManager.Messaging.InboxMessageList.CurrentMessageCount = _messageListEntries.Count;
                // DataStoreManager.Messaging.InboxMessageList.SendRequest();
            })
            {
                name = "ScrollView"
            };

            Add(_scrollView);
        }

        public void ClearMessages()
        {
            _scrollView.Clear();
            _messageListEntries.Clear();
        }

        private void MarkAllMessagesAsRead()
        {
            foreach (var messageListEntry in _messageListEntries)
            {
                messageListEntry.MarkAsRead();
            }
        }
    }
}