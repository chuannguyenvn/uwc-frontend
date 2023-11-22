using UI.Base;
using UI.Views.Messaging.Inbox;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBubblesPanel : AdaptiveElement
    {
        private BubblesColumn _bubblesColumn;
        private InboxContainer _chatBox;

        public ChatBubblesPanel() : base(nameof(ChatBubblesPanel))
        {
            ConfigureUss(nameof(ChatBubblesPanel));

            _bubblesColumn = new BubblesColumn();
            Add(_bubblesColumn);

            _chatBox = new InboxContainer(true);
            Add(_chatBox);
        }
    }
}