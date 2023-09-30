using UI.Common;

namespace UI.Views.Messaging.Inbox
{
    public class InboxSubview : Subview
    {
        private readonly InboxTitleBar _inboxTitleBar;
        private readonly InboxMessageList _inboxMessageList;
        private readonly MessagingField _messagingField;

        public InboxSubview() : base("Inbox")
        {
            _inboxTitleBar = new InboxTitleBar();
            Add(_inboxTitleBar);

            _inboxMessageList = new InboxMessageList();
            Add(_inboxMessageList);

            _messagingField = new MessagingField();
            Add(_messagingField);
        }
    }
}