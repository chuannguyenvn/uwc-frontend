using LocalizationNS;
using Requests;
using Settings;
using SharedLibrary.Communications.OnlineStatus;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxHeader : AdaptiveElement
    {
        // Back button
        private VisualElement _backButton;

        // Avatar
        private TextElement _avatar;

        // Details
        private VisualElement _detailsContainer;
        private TextElement _nameText;
        private TextElement _statusText;

        public InboxHeader() : base(nameof(InboxHeader))
        {
            ConfigureUss(nameof(InboxHeader));

            CreateBackButton();
            CreateAvatar();
            CreateDetails();

            DataStoreManager.OnlineStatus.Status.DataUpdated += DataUpdatedHandler;
        }

        ~InboxHeader()
        {
            DataStoreManager.OnlineStatus.Status.DataUpdated -= DataUpdatedHandler;
        }

        private void CreateBackButton()
        {
            _backButton = new VisualElement { name = "BackButton" };
            if (!Configs.IS_DESKTOP) _backButton.RegisterCallback<ClickEvent>(_ => { GetFirstAncestorOfType<MessagingView>().MobileShowContactList(); });

            Add(_backButton);
        }

        private void CreateAvatar()
        {
            _avatar = new TextElement { name = "Avatar" };
            _avatar.AddToClassList("white-text");
            _avatar.AddToClassList("title-text");
            Add(_avatar);
        }

        private void CreateDetails()
        {
            _detailsContainer = new VisualElement { name = "DetailsContainer" };
            Add(_detailsContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList(Configs.IS_DESKTOP ? "black-text" : "white-text");
            _nameText.text = "";
            _detailsContainer.Add(_nameText);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.AddToClassList("sub-text");
            _statusText.AddToClassList(Configs.IS_DESKTOP ? "black-text" : "white-text");
            _statusText.text = Localization.GetSentence(Sentence.MessagingView.OFFLINE);
            _detailsContainer.Add(_statusText);
        }

        private void DataUpdatedHandler(OnlineStatusBroadcastData data)
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (DataStoreManager.Messaging.InboxMessageList.OtherUserProfile == null) return;

            var otherUserAccountId = DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.Id;
            var onlineAccountIds = DataStoreManager.OnlineStatus.Status.Data.OnlineAccountIds;
            _nameText.text = DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.FirstName + " " +
                             DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.LastName;
            _statusText.text = Localization.GetSentence(onlineAccountIds.Contains(otherUserAccountId) ? Sentence.MessagingView.ONLINE : Sentence.MessagingView.OFFLINE);
            _avatar.text = DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.FirstName[0].ToString();
            _avatar.style.backgroundColor =
                Color.HSVToRGB(DataStoreManager.Messaging.InboxMessageList.OtherUserProfile.AvatarColorHue / 360f, 0.7f, 0.8f);
        }
    }
}