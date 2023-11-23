using UI.Base;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBox : AdaptiveElement
    {
        public ChatBox() : base(nameof(ChatBox))
        {
            ConfigureUss(nameof(ChatBox));
        }
    }
}