using UI.Common;

namespace UI.Views.Messaging.Inbox
{
    public class InboxSubview : Subview
    {
        private readonly InboxContainer _inboxContainer;

        public InboxSubview() : base("Inbox")
        {
            _inboxContainer = new InboxContainer();
            Add(_inboxContainer);
        }
    }
}