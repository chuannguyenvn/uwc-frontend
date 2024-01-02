using System;
using System.Linq;
using Authentication;
using Commons.Communications.Messages;
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

        private DateTime _lastMessageTimestamp;

        // Avatar
        private TextElement _avatar;

        // Details
        private VisualElement _detailsContainer;
        private TextElement _nameText;
        private TextElement _previewText;

        public ContactListEntry(Message message) : base(
            nameof(ContactListEntry))
        {
            _lastMessageTimestamp = message.Timestamp;

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

            if (message.IsSeen || message.SenderProfileId == AuthenticationManager.Instance.UserAccountId) MarkAsRead();
            RegisterCallbacks();

            DataStoreManager.Messaging.ContactList.UserSentMessages += UserSentMessagesHandler;
        }

        private void UserSentMessagesHandler(SendMessageBroadcastData data)
        {
            if (data.Messages.Count == 0) return;
            var latestMessage = data.Messages.OrderByDescending(message => message.Timestamp).First();
            if (latestMessage.ReceiverProfileId != UserProfile.Id && latestMessage.SenderProfileId != UserProfile.Id) return;

            if (latestMessage.IsSeen || latestMessage.SenderProfileId == AuthenticationManager.Instance.UserAccountId) MarkAsRead();
            schedule.Execute(() =>
            {
                UpdateDetails(latestMessage.Content, latestMessage.Timestamp,
                    latestMessage.SenderProfileId == AuthenticationManager.Instance.UserAccountId);
            });
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
                // if (DataStoreManager.Messaging.InboxMessageList.OtherUserProfile != null && UserProfile != null &&
                //     (Configs.IS_DESKTOP && DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.Id == UserProfile.Id)) return;

                ContactList.IsShow = false;

                GetFirstAncestorOfType<MessagingView>().Q<InboxContainer>().SwitchInbox(UserProfile, () =>
                {
                    if (!Configs.IS_DESKTOP) GetFirstAncestorOfType<MessagingView>().MobileShowInbox();
                });
            });
        }

        private void UpdateDetails(string content, DateTime timestamp, bool isFromUser)
        {
            if (timestamp <= _lastMessageTimestamp) return;
            _lastMessageTimestamp = timestamp;

            var previewText = timestamp.ToLocalTime().ToString(DateTime.Now.Date == timestamp.Date ? "HH:mmtt" : "HH:mmtt dd/MM") + " | " +
                              (isFromUser ? "You: " : "") + content;

            _previewText.text = previewText;

            if (!isFromUser && DataStoreManager.Messaging.InboxMessageList.OtherUserProfile != null &&
                DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.Id != UserProfile.Id)
            {
                EnableInClassList("read", false);
            }
            else
            {
                EnableInClassList("read", true);
            }
        }

        public void MarkAsRead()
        {
            EnableInClassList("read", true);
        }
    }
}