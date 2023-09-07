using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class MessagesScreen : VisualElement
    {
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MessagesScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion

        private ContactsContainer _contactsContainer;
        private InboxContainer _inboxContainer;

        public MessagesScreen()
        {
            AddToClassList("messages-screen");

            _contactsContainer = new ContactsContainer();
            Add(_contactsContainer);

            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }
    }
}