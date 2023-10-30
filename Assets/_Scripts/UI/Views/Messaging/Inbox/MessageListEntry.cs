using Authentication;
using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class MessageListEntry : AdaptiveElement
    {
        private VisualElement _contentContainer;
        private TextElement _contentText;

        private TextElement _timestampText;

        public MessageListEntry(Message message) : base(nameof(MessageListEntry))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/MessageListEntry"));

            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _contentText = new TextElement { name = "ContentText" };
            _contentText.text = message.Content;
            _contentContainer.Add(_contentText);

            _timestampText = new TextElement { name = "TimestampText" };

            _timestampText.text = message.Timestamp.ToString("dd/MM/yyyy HH:mm");
            Add(_timestampText);

            if (message.SenderAccountId == AuthenticationManager.Instance.UserAccountId)
            {
                _contentText.AddToClassList("white-text");
                AddToClassList("sent-message");
            }
            else
            {
                _contentText.AddToClassList("black-text");
                AddToClassList("received-message");
            }

            _contentText.AddToClassList("normal-text");
            _timestampText.AddToClassList("sub-text");
            _timestampText.AddToClassList("grey-text");
        }
    }
}