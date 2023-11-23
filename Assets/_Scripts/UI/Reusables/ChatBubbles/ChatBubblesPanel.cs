using Commons.Models;
using Requests;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UnityEngine.UIElements;

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
            
            HideChatBox();
        }

        public void FocusInbox(UserProfile userProfile)
        {
            _bubblesColumn.OpenBubble(userProfile);
            _chatBox.SwitchInbox(userProfile);
            ShowChatBox();
        }
        
        public void ShowChatBox()
        {
            _chatBox.style.display = DisplayStyle.Flex;
        }
        
        public void HideChatBox()
        {
            _chatBox.style.display = DisplayStyle.None;
        }
    }
}