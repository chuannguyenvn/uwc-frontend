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
                Remove(_focusedBubbles[^1]);
                _focusedBubbles.RemoveAt(_focusedBubbles.Count - 1);

                _focusedUserProfiles.RemoveAt(_focusedUserProfiles.Count - 1);
            }

            _focusedUserProfiles.Insert(0, userProfile);

            var bubble = new ChatBubble(userProfile);
            _focusedBubbles.Insert(0, bubble);
            Add(bubble);
            bubble.SendToBack();
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