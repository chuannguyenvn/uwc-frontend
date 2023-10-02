using Constants;
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
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/MessagingView"));
            if(Configs.IS_DESKTOP) AddToClassList("full-view");

            _contactList = new ContactList();
            Add(_contactList);
            
            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }
    }
}