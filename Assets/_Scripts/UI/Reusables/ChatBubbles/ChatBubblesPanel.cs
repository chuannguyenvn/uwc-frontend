using Commons.Models;
using Settings;
using UI.Base;
using UI.Views.Messaging.Inbox;
using UnityEngine.UIElements;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBubblesPanel : AdaptiveElement
    {
        private BubblesColumn _bubblesColumn;
        private InboxContainer _chatBox;

        private UserProfile _focusedUserProfile;

        public ChatBubblesPanel() : base(nameof(ChatBubblesPanel))
        {
            ConfigureUss(nameof(ChatBubblesPanel));

            _bubblesColumn = new BubblesColumn();
            Add(_bubblesColumn);

            _chatBox = new InboxContainer(true);
            Add(_chatBox);

            Root.Instance.ViewFocused += type =>
            {
                if (type == ViewType.Map && _focusedUserProfile != null)
                {
                    _chatBox.SwitchInbox(_focusedUserProfile);
                }
            };

            HideChatBox();
        }

        public void FocusInbox(UserProfile userProfile)
        {
            _focusedUserProfile = userProfile;
            _bubblesColumn.OpenBubble(_focusedUserProfile);
            // if (Root.Instance.ActiveViewType == ViewType.Map)
            // {
            //     _chatBox.SwitchInbox(_focusedUserProfile);
            // }

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