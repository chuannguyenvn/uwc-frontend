﻿using Commons.Models;
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
            _contentText.AddToClassList("normal-text");
            _contentText.text = message.Content;
            _contentContainer.Add(_contentText);

            _timestampText = new TextElement { name = "TimestampText" };
            _timestampText.AddToClassList("sub-text");
            _timestampText.AddToClassList("grey-text");
            _timestampText.text = message.Timestamp.ToString("dd/MM/yyyy HH:mm");
            Add(_timestampText);

            if (Random.Range(0, 2) == 0)
            {
                _contentText.AddToClassList("white-text");
                _contentText.AddToClassList("sent-message");
                _contentContainer.AddToClassList("sent-message");
                AddToClassList("sent-message");
            }
            else
            {
                _contentText.AddToClassList("black-text");
                _contentText.AddToClassList("received-message");
                _contentContainer.AddToClassList("received-message");
                AddToClassList("received-message");
            }
        }
    }
}