using System;
using Authentication;
using Commons.Models;
using Requests;
using Settings;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactListEntry : AnimatedButton
    {
        public UserProfile UserProfile { get; }
        public string FullName => UserProfile.FirstName + " " + UserProfile.LastName;
        public string PreviewMessage { get; }

        // Avatar
        private Image _avatar;

        // Details
        private VisualElement _detailsContainer;
        private TextElement _nameText;
        private TextElement _previewText;

        public ContactListEntry(Message message) : base(
            nameof(ContactListEntry))
        {
            UserProfile = message.SenderProfileId == AuthenticationManager.Instance.UserAccountId
                ? message.ReceiverUserProfile
                : message.SenderUserProfile;

            PreviewMessage = message.Content;

            ConfigureUss(nameof(ContactListEntry));

            AddToClassList("white-button");
            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");

            // CreateAvatar();
            CreateDetails(UserProfile.FirstName + " " + UserProfile.LastName, message.Content, message.Timestamp,
                message.SenderProfileId == AuthenticationManager.Instance.UserAccountId);

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
                RegisterCallback<ClickEvent>(_ => { GetFirstAncestorOfType<MessagingView>().MobileShowInbox(); });
            }

            RegisterCallback<ClickEvent>(_ => GetFirstAncestorOfType<MessagingView>().Q<InboxContainer>().SwitchInbox(UserProfile));
        }
    }
}