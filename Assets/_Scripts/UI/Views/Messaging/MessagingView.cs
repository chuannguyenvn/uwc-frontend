using Constants;
using UI.Common;
using UI.Views.Messaging.Contacts;
using UI.Views.Messaging.Inbox;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Views.Messaging
{
    public class MessagingView : FullScreenView
    {
        private readonly ContactsSubview _contactsSubview;
        private readonly InboxSubview _inboxSubview;

        public MessagingView() : base("Messaging")
        {
            AddToClassList("messaging");
            if (!Configs.IS_DESKTOP) AddToClassList("full-screen-view");

            _contactsSubview = new ContactsSubview();
            Add(_contactsSubview);
            
            _inboxSubview = new InboxSubview();
            Add(_inboxSubview);
        }
    }
}