using UI.Common;
using UI.Messaging.Contacts;
using UI.Messaging.Inbox;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messaging
{
    public class MessagingView : FullScreenView
    {
        private readonly ContactsList _contactsList;
        private readonly InboxContainer _inboxContainer;

        public MessagingView() : base("Messaging")
        {
            AddToClassList("messaging");

            _contactsList = new ContactsList();
            Add(_contactsList);

            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MessagingView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}