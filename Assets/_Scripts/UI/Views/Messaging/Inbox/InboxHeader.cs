﻿using Requests;
using Settings;
using SharedLibrary.Communications.OnlineStatus;
using UI.Base;
using UI.Views.Messaging.Contacts;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxHeader : AdaptiveElement
    {
        // Back button
        private VisualElement _backButton;

        // Avatar
        private VisualElement _avatar;

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
            if (!Configs.IS_DESKTOP)
            {
                _backButton.RegisterCallback<ClickEvent>(_ =>
                {
                    RegisterCallback<ClickEvent>(_ =>
                    {
                        GetFirstAncestorOfType<MessagingView>().Q<InboxContainer>().style.display = DisplayStyle.None;
                        GetFirstAncestorOfType<MessagingView>().Q<ContactList>().style.display = DisplayStyle.Flex;
                    });
                });
            }

            Add(_backButton);
        }

        private void CreateAvatar()
        {
            _avatar = new VisualElement { name = "Avatar" };
            Add(_avatar);
        }

        private void CreateDetails()
        {
            _detailsContainer = new VisualElement { name = "DetailsContainer" };
            Add(_detailsContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("white-text");
            _nameText.text = "";
            _detailsContainer.Add(_nameText);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.AddToClassList("sub-text");
            _statusText.AddToClassList("white-text");
            _statusText.text = "Offline";
            _detailsContainer.Add(_statusText);
        }

        private void DataUpdatedHandler(OnlineStatusBroadcastData data)
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            var otherUserAccountId = DataStoreManager.Messaging.InboxMessageList.OtherUserAccountId;
            var onlineAccountIds = DataStoreManager.OnlineStatus.Status.Data.OnlineAccountIds;
            _statusText.text = onlineAccountIds.Contains(otherUserAccountId) ? "Online" : "Offline";
        }

        public void UpdateName(string name)
        {
            _nameText.text = name;
        }
    }
}