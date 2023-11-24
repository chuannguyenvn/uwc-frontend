using Commons.Communications.Messages;
using Requests;
using UI.Base;
using UI.Reusables;

namespace UI.Views.Messaging.Inbox
{
    public class MessageList : AdaptiveElement
    {
        private ScrollViewWithShadow _scrollView;

        public MessageList() : base(nameof(MessageList))
        {
            ConfigureUss(nameof(MessageList));

            CreateScrollView();

            DataStoreManager.Messaging.InboxMessageList.DataUpdated += DataUpdatedHandler;
        }

        ~MessageList()
        {
            DataStoreManager.Messaging.InboxMessageList.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(GetMessagesBetweenTwoUsersResponse data)
        {
            _scrollView.Clear();
            data.Messages.Sort((a, b) => a.Timestamp.CompareTo(b.Timestamp));
            foreach (var message in data.Messages)
            {
                _scrollView.AddToScrollView(new MessageListEntry(message));
            }

            _scrollView.ScrollToLast();
        }

        private void CreateScrollView()
        {
            _scrollView = new ScrollViewWithShadow(ShadowType.InnerTop);
            Add(_scrollView);
        }
    }
}