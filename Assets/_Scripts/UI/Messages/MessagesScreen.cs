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

        private ContactsList _contactsList;
        private InboxContainer _inboxContainer;

        public MessagesScreen()
        {
            AddToClassList("messages-screen");

            _contactsList = new ContactsList();
            Add(_contactsList);

            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }
    }
}