using Commons.Models;
using UI.Base;

namespace UI.Reusables.ChatBubbles
{
    public class BubblesColumn : AdaptiveElement
    {
        public BubblesColumn() : base(nameof(BubblesColumn))
        {
            ConfigureUss(nameof(BubblesColumn));
        }
        
        public void FocusBubble(UserProfile userProfile)
        {
          
        }
    }
}