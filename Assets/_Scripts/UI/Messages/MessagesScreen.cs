using UI.Commons;
using UI.Messages.Contacts;
using UI.Messages.Inbox;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class MessagesScreen : FullScreenPanel
    {
        private readonly ContactsList _contactsList;
        private readonly InboxContainer _inboxContainer;

        public MessagesScreen()
        {
            AddToClassList("messages-screen");

            _contactsList = new ContactsList();
            Add(_contactsList);

            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MessagesScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : FullScreenPanel.UxmlTraits
        {
        }

        #endregion
    }
}