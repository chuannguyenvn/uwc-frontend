using Commons.Communications.Messages;
using Commons.Models;
using Requests;
using UI.Base;
using UI.Views.Messaging.Contacts;
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

            for (int i = 0; i < 30; i++)
            {
                ScrollView.Add(new MessageListEntry(new Message()
                {
                    Content = Random.Range(0, 2) == 0
                        ? "Text content of the message"
                        : "Text content of the message lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
                    Timestamp = System.DateTime.Now,
                }));
            }

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
                ScrollView.Add(new MessageListEntry(new Message()
                {
                    Content = message.Content,
                    Timestamp = message.Timestamp,
                }));
            }
        }
    }
}