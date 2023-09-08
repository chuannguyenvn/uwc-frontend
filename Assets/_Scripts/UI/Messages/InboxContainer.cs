using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class InboxContainer : VisualElement
    {
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

        private InboxTitleBar _inboxTitleBar;
        private InboxMessageList _inboxMessageList;
        private MessagingField _messagingField;

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
    }
}