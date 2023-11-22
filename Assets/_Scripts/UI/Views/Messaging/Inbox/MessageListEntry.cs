using Authentication;
using Commons.Models;
using UI.Base;
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
            ConfigureUss(nameof(MessageListEntry));

            CreateContent(message);
            CreateTimestamp(message);
        }

        private void CreateContent(Message message)
        {
            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _contentText = new TextElement { name = "ContentText" };
            _contentText.AddToClassList("normal-text");
            _contentText.text = message.Content;
            _contentContainer.Add(_contentText);

            if (message.SenderProfileId == AuthenticationManager.Instance.UserAccountId)
            {
                _contentText.AddToClassList("white-text");
                AddToClassList("sent-message");
            }
            else
            {
                _contentText.AddToClassList("black-text");
                AddToClassList("received-message");
            }
        }

        private void CreateTimestamp(Message message)
        {
            _timestampText = new TextElement { name = "TimestampText" };
            _timestampText.AddToClassList("sub-text");
            _timestampText.AddToClassList("grey-text");
            _timestampText.text = message.Timestamp.ToString("dd/MM HH:mm");
            Add(_timestampText);
        }
    }
}