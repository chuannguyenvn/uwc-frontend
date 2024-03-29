using Requests;
using Settings;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UI.Views.Messaging.Contacts;
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

        private void CreateContactList()
        {
            _contactList = new ContactList();
            Add(_contactList);
        }

        private void CreateInbox()
        {
            _inboxContainer = new InboxContainer(false);
            Add(_inboxContainer);

            if (!Configs.IS_DESKTOP) _inboxContainer.style.display = DisplayStyle.None;
        }

        public override void FocusView()
        {
            DataStoreManager.Messaging.ContactList.SendRequest();
        }

        public override void UnfocusView()
        {
        }

        public void MobileShowInbox()
        {
            ContactList.IsShow = false;
            _contactList.style.display = DisplayStyle.None;
            _inboxContainer.style.display = DisplayStyle.Flex;
        }

        public void MobileShowContactList()
        {
            ContactList.IsShow = true;
            _contactList.style.display = DisplayStyle.Flex;
            _inboxContainer.style.display = DisplayStyle.None;
        }
    }
}