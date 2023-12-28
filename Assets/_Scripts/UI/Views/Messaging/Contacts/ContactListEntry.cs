using System;
using Authentication;
using Commons.Models;
using Requests;
using Settings;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Contacts
{
    public class ContactListEntry : AnimatedButton
    {
        public UserProfile UserProfile { get; }
        public string FullName => UserProfile.FirstName + " " + UserProfile.LastName;
        public string PreviewMessage { get; }

        // Avatar
        private TextElement _avatar;

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
            AddToClassList(Configs.IS_DESKTOP ? "rounded-button-16px" : "rounded-button-32px");

            CreateImage(UserProfile);
            CreateDetails(UserProfile.FirstName + " " + UserProfile.LastName, message.Content, message.Timestamp,
                message.SenderProfileId == AuthenticationManager.Instance.UserAccountId);

            RegisterCallbacks();
        }

        private void CreateImage(UserProfile profile)
        {
            _avatar = new TextElement { name = "Avatar" };
            _avatar.AddToClassList("white-text");
            _avatar.AddToClassList("title-text");
            _avatar.text = profile.FirstName[0].ToString();
            _avatar.style.backgroundColor = Color.HSVToRGB(profile.AvatarColorHue / 360f, 0.7f, 0.8f);
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
            _previewText.text = timestamp.ToLocalTime().ToString(DateTime.Now.Date == timestamp.Date ? "HH:mmtt" : "HH:mmtt dd/MM") + " | " +
                                (isFromUser ? "You: " : "") + messageContent;
            _detailsContainer.Add(_previewText);
        }

        private void RegisterCallbacks()
        {
            RegisterCallback<ClickEvent>(_ =>
            {
                if (DataStoreManager.Messaging.InboxMessageList.OtherUserProfile != null && UserProfile != null &&
                    DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.Id == UserProfile.Id) return;

                GetFirstAncestorOfType<MessagingView>().Q<InboxContainer>().SwitchInbox(UserProfile, () =>
                {
                    if (!Configs.IS_DESKTOP) GetFirstAncestorOfType<MessagingView>().MobileShowInbox();
                });
            });
        }
    }
}