using System;
using Commons.Models;
using Constants;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactListEntry : AdaptiveElement
    {
        private Image _image;

        private VisualElement _textContainer;
        private TextElement _nameText;
        private TextElement _previewText;

        public ContactListEntry(string contactName, string messageContent, DateTime timestamp, bool isFromUser) : base(nameof(ContactListEntry))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Contacts/ContactListEntry"));
            AddToClassList("list-entry");

            _image = new Image { name = "Avatar" };
            Add(_image);

            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("black-text");
            _nameText.text = contactName;
            _textContainer.Add(_nameText);

            _previewText = new TextElement { name = "PreviewText" };
            _previewText.AddToClassList("sub-text");
            _previewText.AddToClassList("grey-text");
            _previewText.text = (isFromUser ? "You: " : "") + messageContent + " - " + timestamp.ToString();
            _textContainer.Add(_previewText);

            if (!Configs.IS_DESKTOP)
            {
                RegisterCallback<MouseUpEvent>(_ =>
                {
                    GetFirstAncestorOfType<MessagingView>().InboxContainer.style.display = DisplayStyle.Flex;
                    GetFirstAncestorOfType<MessagingView>().ContactList.style.display = DisplayStyle.None;
                });
            }
        }
    }
}