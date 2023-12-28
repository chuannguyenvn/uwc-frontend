using System;
using Authentication;
using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class MessageListEntry : AdaptiveElement
    {
        public readonly Message Message;
        private VisualElement _contentContainer;
        private TextElement _contentText;

        private TextElement _timestampText;

        public MessageListEntry(Message message) : base(nameof(MessageListEntry))
        {
            Message = message;
            ConfigureUss(nameof(MessageListEntry));

            CreateContent(message);
            CreateTimestamp(message);
            SetSeenMode(message);
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
            _timestampText.text = message.Timestamp.ToLocalTime().ToString(DateTime.Now.Date == message.Timestamp.Date ? "HH:mmtt" : "HH:mmtt dd/MM");
            Add(_timestampText);
        }

        private void SetSeenMode(Message message)
        {
            if (message.SenderProfileId == AuthenticationManager.Instance.UserAccountId && !message.IsSeen)
            {
                _contentContainer.AddToClassList("not-seen");
                _contentText.AddToClassList("black-text");
                _contentText.RemoveFromClassList("white-text");
            }
        }

        public void MarkAsRead()
        {
            if (Message.SenderProfileId != AuthenticationManager.Instance.UserAccountId) return;

            Debug.Log(Message.Content);

            _contentContainer.RemoveFromClassList("not-seen");
            _contentText.RemoveFromClassList("black-text");
            _contentText.AddToClassList("white-text");
        }
    }
}