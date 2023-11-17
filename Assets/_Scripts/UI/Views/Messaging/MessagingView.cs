using Requests;
using Settings;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UI.Views.Messaging.Contacts;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging
{
    public class MessagingView : View
    {
        private ContactList _contactList;
        private InboxContainer _inboxContainer;

        public MessagingView() : base(nameof(MessagingView))
        {
            ConfigureUss(nameof(MessagingView));

            if (Configs.IS_DESKTOP) AddToClassList("full-view");

            CreateContactList();
            CreateInbox();
        }

        private void CreateInbox()
        {
            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }

        private void CreateContactList()
        {
            _contactList = new ContactList();
            Add(_contactList);

            if (!Configs.IS_DESKTOP) _inboxContainer.style.display = DisplayStyle.None;
        }

        public override void FocusView()
        {
            DataStoreManager.Messaging.ContactList.Focus();
            _inboxContainer.Q<InboxHeader>().UpdateStatus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.Messaging.ContactList.Unfocus();
        }
    }
}