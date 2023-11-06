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
        public ContactList ContactList;
        public InboxContainer InboxContainer;

        public MessagingView() : base(nameof(MessagingView))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/MessagingView"));
            if (Configs.IS_DESKTOP) AddToClassList("full-view");

            ContactList = new ContactList();
            Add(ContactList);

            InboxContainer = new InboxContainer();
            Add(InboxContainer);

            if (!Configs.IS_DESKTOP) InboxContainer.style.display = DisplayStyle.None;
        }

        public override void FocusView()
        {
            DataStoreManager.Messaging.ContactList.Focus();
            InboxContainer.InboxHeader.UpdateStatus();
        }

        public override void UnfocusView()
        {
            DataStoreManager.Messaging.ContactList.Unfocus();
        }
    }
}