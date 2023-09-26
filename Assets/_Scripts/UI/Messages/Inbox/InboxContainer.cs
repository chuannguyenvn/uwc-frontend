using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messaging.Inbox
{
    public class InboxContainer : VisualElement
    {
        private readonly InboxTitleBar _inboxTitleBar;
        private readonly InboxMessageList _inboxMessageList;
        private readonly MessagingField _messagingField;

        public InboxContainer()
        {
            name = "InboxContainer";

            _inboxTitleBar = new InboxTitleBar();
            Add(_inboxTitleBar);

            _inboxMessageList = new InboxMessageList();
            Add(_inboxMessageList);

            _messagingField = new MessagingField();
            Add(_messagingField);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<InboxContainer, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}