using UI.Base;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBubble : AdaptiveElement
    {
        public int OtherUserId { get; }
        public string FullName { get; }
        
        public ChatBubble() : base(nameof(ChatBubble))
        {
            ConfigureUss(nameof(ChatBubble));
        }
    }
}