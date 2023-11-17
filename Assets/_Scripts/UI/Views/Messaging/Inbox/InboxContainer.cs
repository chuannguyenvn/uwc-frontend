using UI.Base;

namespace UI.Views.Messaging.Inbox
{
    public class InboxContainer : AdaptiveElement
    {
        private InboxHeader _inboxHeader;
        private MessageList _messageList;
        private InputBar _inputBar;

        public InboxContainer() : base(nameof(InboxContainer))
        {
            ConfigureUss(nameof(InboxContainer));

            AddToClassList("full-view");

            CreateInboxHeader();
            CreateMessageList();
            CreateInputBar();
        }

        private void CreateInboxHeader()
        {
            _inboxHeader = new InboxHeader();
            Add(_inboxHeader);
        }

        private void CreateMessageList()
        {
            _messageList = new MessageList();
            Add(_messageList);
        }

        private void CreateInputBar()
        {
            _inputBar = new InputBar();
            Add(_inputBar);
        }

        public void SwitchInbox()
        {
        }
    }
}