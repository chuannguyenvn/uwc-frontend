using Commons.Communications.Messages;
using Commons.Models;
using Requests;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class MessageList : AdaptiveElement
    {
        private ScrollView ScrollView;

        public MessageList() : base(nameof(MessageList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/MessageList"));

            ScrollView = new ScrollView();
            ScrollView.AddToClassList("list-view");
            ScrollView.verticalScroller.value = ScrollView.verticalScroller.highValue > 0 ? ScrollView.verticalScroller.highValue : 0;
            Add(ScrollView);

            DataStoreManager.Messaging.InboxMessageList.DataUpdated += DataUpdatedHandler;
        }

        ~MessageList()
        {
            DataStoreManager.Messaging.InboxMessageList.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(GetMessagesBetweenTwoUsersResponse data)
        {
            ScrollView.Clear();
            foreach (var message in data.Messages)
            {
                ScrollView.Add(new MessageListEntry(message));
            }

            if (ScrollView.verticalScroller.highValue > 0 &&
                Mathf.Abs(ScrollView.verticalScroller.value - ScrollView.verticalScroller.highValue) < 200f)
            {
                ScrollView.verticalScroller.value = ScrollView.verticalScroller.highValue;
            }
        }
    }
}