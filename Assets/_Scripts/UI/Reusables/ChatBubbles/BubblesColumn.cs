using System.Collections.Generic;
using System.Linq;
using Commons.Models;
using UI.Base;

namespace UI.Reusables.ChatBubbles
{
    public class BubblesColumn : AdaptiveElement
    {
        private const int MAX_BUBBLES = 5;
        private List<UserProfile> _focusedUserProfiles = new();
        private List<ChatBubble> _focusedBubbles = new();

        public BubblesColumn() : base(nameof(BubblesColumn))
        {
            ConfigureUss(nameof(BubblesColumn));
        }

        public void OpenBubble(UserProfile userProfile)
        {
            CloseBubble(userProfile);

            if (_focusedUserProfiles.Count >= MAX_BUBBLES)
            {
                _focusedBubbles.RemoveAt(0);
                Remove(_focusedBubbles[0]);

                _focusedUserProfiles.RemoveAt(0);
            }

            _focusedUserProfiles.Add(userProfile);
            
            var bubble = new ChatBubble(userProfile);
            _focusedBubbles.Add(bubble);
            Add(bubble);
            ChatBubble.FocusedBubble = bubble;
        }
        
        public void CloseBubble(UserProfile userProfile)
        {
            if (_focusedUserProfiles.Any(profile => profile.Id == userProfile.Id))
            {
                var index = _focusedUserProfiles.FindIndex(profile => profile.Id == userProfile.Id);
                _focusedUserProfiles.RemoveAt(index);

                Remove(_focusedBubbles[index]);
                _focusedBubbles.RemoveAt(index);
            }
        }
    }
}