using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class MessageList : AdaptiveElement
    {
        private ScrollView _scrollView;

        public MessageList() : base(nameof(MessageList))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/MessageList"));

            _scrollView = new ScrollView();
            _scrollView.AddToClassList("list-view");
            Add(_scrollView);

            for (int i = 0; i < 30; i++)
            {
                _scrollView.Add(new MessageListEntry(new Message()
                {
                    Content = Random.Range(0, 2) == 0
                        ? "Text content of the message"
                        : "Text content of the message lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam",
                    Timestamp = System.DateTime.Now,
                }));
            }
        }
    }
}