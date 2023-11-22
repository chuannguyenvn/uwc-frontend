using UI.Base;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBubblesPanel : AdaptiveElement
    {
        private BubblesColumn _bubblesColumn;
        private ChatBox _chatBox;

        public ChatBubblesPanel() : base(nameof(ChatBubblesPanel))
        {
            ConfigureUss(nameof(ChatBubblesPanel));
            
            _bubblesColumn = new BubblesColumn();
            Add(_bubblesColumn);

            _chatBox = new ChatBox();
            Add(_chatBox);
        }
    }
}