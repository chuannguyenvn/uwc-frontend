using UI.Base;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBubble : AdaptiveElement
    {
        public ChatBubble() : base(nameof(ChatBubble))
        {
            ConfigureUss(nameof(ChatBubble));
        }
    }
}