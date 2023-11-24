using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables.ChatBubbles
{
    public class ChatBubble : AdaptiveElement
    {
        public static ChatBubble FocusedBubble;
        public UserProfile UserProfile { get; }
        public int UnreadMessagesCount { get; set; }

        private TextElement _avatar;
        private TextElement _notificationDot;
        private VisualElement _closeIcon;

        public ChatBubble(UserProfile userProfile) : base(nameof(ChatBubble))
        {
            UserProfile = userProfile;

            ConfigureUss(nameof(ChatBubble));

            CreateAvatar();
            CreateNotificationDot();
            RegisterCallbacks();
        }

        private void CreateAvatar()
        {
            _avatar = new TextElement { name = "Avatar" };
            _avatar.AddToClassList("white-text");
            _avatar.AddToClassList("title-text");
            _avatar.text = UserProfile.FirstName[0].ToString();
            _avatar.style.backgroundColor = Color.HSVToRGB(UserProfile.AvatarColorHue / 360f, 0.7f, 0.8f);
            Add(_avatar);
        }

        private void CreateNotificationDot()
        {
            _notificationDot = new TextElement { name = "NotificationDot" };
            _notificationDot.AddToClassList("normal-text");
            _notificationDot.AddToClassList("white-text");
            _notificationDot.AddToClassList("notification-dot");
            _avatar.Add(_notificationDot);

            _closeIcon = new VisualElement { name = "CloseIcon" };
            _notificationDot.Add(_closeIcon);

            _notificationDot.style.display = DisplayStyle.None;
        }

        private void RegisterCallbacks()
        {
            RegisterCallback<MouseOverEvent>(_ =>
            {
                _notificationDot.style.display = DisplayStyle.Flex;

                if (UnreadMessagesCount > 0)
                {
                    _closeIcon.style.display = DisplayStyle.None;
                    _notificationDot.text = UnreadMessagesCount.ToString();
                }
                else
                {
                    _closeIcon.style.display = DisplayStyle.Flex;
                    _notificationDot.text = "";
                }
            });
            RegisterCallback<MouseOutEvent>(_ => { _notificationDot.style.display = DisplayStyle.None; });
            RegisterCallback<ClickEvent>(_ =>
            {
                if (FocusedBubble == this)
                {
                    FocusedBubble = null;
                    GetFirstAncestorOfType<ChatBubblesPanel>().HideChatBox();
                }
                else
                {
                    FocusedBubble = this;
                    GetFirstAncestorOfType<ChatBubblesPanel>().FocusInbox(UserProfile);
                }
            });

            _closeIcon.RegisterCallback<ClickEvent>(_ =>
            {
                GetFirstAncestorOfType<ChatBubblesPanel>().HideChatBox();
                GetFirstAncestorOfType<ChatBubblesPanel>().Q<BubblesColumn>().CloseBubble(UserProfile);
            });
        }
    }
}