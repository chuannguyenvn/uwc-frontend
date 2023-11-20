using System;
using Requests;
using Settings;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactListEntry : AnimatedButton
    {
        public string ContactName { get; }
        public string PreviewMessage { get; }
        private readonly int _otherUserId;

        // Avatar
        private Image _avatar;

        // Details
        private VisualElement _detailsContainer;
        private TextElement _nameText;
        private TextElement _previewText;

        public ContactListEntry(int otherUserId, string contactName, string previewMessage, DateTime timestamp, bool isFromUser) : base(
            nameof(ContactListEntry))
        {
            ContactName = contactName;
            PreviewMessage = previewMessage;
            _otherUserId = otherUserId;

            ConfigureUss(nameof(ContactListEntry));

            AddToClassList("white-button");
            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");

            // CreateAvatar();
            CreateDetails(contactName, previewMessage, timestamp, isFromUser);

            RegisterCallbacks();
        }

        private void CreateAvatar()
        {
            _avatar = new Image { name = "Avatar" };
            Add(_avatar);
        }

        private void CreateDetails(string contactName, string messageContent, DateTime timestamp, bool isFromUser)
        {
            _detailsContainer = new VisualElement { name = "DetailsContainer" };
            Add(_detailsContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("black-text");
            _nameText.text = contactName;
            _detailsContainer.Add(_nameText);

            _previewText = new TextElement { name = "PreviewText" };
            _previewText.AddToClassList("sub-text");
            _previewText.AddToClassList("grey-text");
            _previewText.text = (isFromUser ? "You: " : "") + messageContent + " - " + timestamp.ToString("dd/mm HH:mm");
            _detailsContainer.Add(_previewText);
        }

        private void RegisterCallbacks()
        {
            if (!Configs.IS_DESKTOP)
            {
                RegisterCallback<MouseUpEvent>(_ =>
                {
                    GetFirstAncestorOfType<MessagingView>().Q<InboxContainer>().style.display = DisplayStyle.Flex;
                    GetFirstAncestorOfType<MessagingView>().Q<ContactList>().style.display = DisplayStyle.None;
                });
            }

            RegisterCallback<ClickEvent>(_ => ShowMessages());
        }

        public void ShowMessages()
        {
            DataStoreManager.Messaging.InboxMessageList.SendRequest();
            DataStoreManager.Messaging.InboxMessageList.OtherUserAccountId = _otherUserId;
            GetFirstAncestorOfType<MessagingView>().Q<InboxHeader>().UpdateName(_nameText.text);
            GetFirstAncestorOfType<MessagingView>().Q<InboxHeader>().UpdateStatus();
        }
    }
}