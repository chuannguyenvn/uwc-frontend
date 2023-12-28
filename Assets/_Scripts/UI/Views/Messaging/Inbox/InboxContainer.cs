using System;
using Commons.Models;
using Requests;
using UI.Base;

namespace UI.Views.Messaging.Inbox
{
    public class InboxContainer : AdaptiveElement
    {
        private InboxHeader _inboxHeader;
        private MessageList _messageList;
        private InputBar _inputBar;

        public InboxContainer(bool isBubbleChatbox) : base(nameof(InboxContainer))
        {
            ConfigureUss(nameof(InboxContainer));

            if (!isBubbleChatbox) AddToClassList("full-view");

            CreateInboxHeader();
            CreateMessageList();
            CreateInputBar();
        }

        private void CreateInboxHeader()
        {
            _inboxHeader = new InboxHeader();
            Add(_inboxHeader);
        }

        private void CreateMessageList()
        {
            _messageList = new MessageList();
            Add(_messageList);
        }

        private void CreateInputBar()
        {
            _inputBar = new InputBar();
            Add(_inputBar);
        }

        public void SwitchInbox(UserProfile userProfile, Action callback = null)
        {
            _messageList.ClearMessages();
            DataStoreManager.Messaging.InboxMessageList.OtherUserProfile = userProfile;
            DataStoreManager.Messaging.InboxMessageList.CurrentMessageCount = 0;
            DataStoreManager.Messaging.InboxMessageList.SendRequest(callback);
            _inboxHeader.UpdateStatus();
        }
    }
}