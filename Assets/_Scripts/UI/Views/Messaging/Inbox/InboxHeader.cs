using Requests;
using Settings;
using SharedLibrary.Communications.OnlineStatus;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxHeader : AdaptiveElement
    {
        public VisualElement BackButton;

        public VisualElement Avatar;

        public VisualElement TextContainer;
        public TextElement NameText;
        public TextElement StatusText;

        public InboxHeader() : base(nameof(InboxHeader))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/InboxHeader"));

            BackButton = new VisualElement { name = "BackButton" };
            if (!Configs.IS_DESKTOP)
            {
                BackButton.RegisterCallback<ClickEvent>(_ =>
                {
                    RegisterCallback<ClickEvent>(_ =>
                    {
                        GetFirstAncestorOfType<MessagingView>().InboxContainer.style.display = DisplayStyle.None;
                        GetFirstAncestorOfType<MessagingView>().ContactList.style.display = DisplayStyle.Flex;
                    });
                });
            }

            Add(BackButton);

            Avatar = new VisualElement { name = "Avatar" };
            Add(Avatar);

            TextContainer = new VisualElement { name = "TextContainer" };
            Add(TextContainer);

            NameText = new TextElement { name = "NameText" };
            NameText.AddToClassList("normal-text");
            NameText.AddToClassList("white-text");
            NameText.text = "";
            TextContainer.Add(NameText);

            StatusText = new TextElement { name = "StatusText" };
            StatusText.AddToClassList("sub-text");
            StatusText.AddToClassList("white-text");
            StatusText.text = "Offline";
            TextContainer.Add(StatusText);

            DataStoreManager.OnlineStatus.Status.DataUpdated += DataUpdatedHandler;
        }

        ~InboxHeader()
        {
            DataStoreManager.OnlineStatus.Status.DataUpdated -= DataUpdatedHandler;
        }

        private void DataUpdatedHandler(OnlineStatusBroadcastData data)
        {
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            var otherUserAccountId = DataStoreManager.Messaging.InboxMessageList.OtherUserAccountId;

            if (DataStoreManager.OnlineStatus.Status.Data.OnlineAccountIds.Contains(otherUserAccountId)) StatusText.text = "Online";
            else StatusText.text = "Offline";
        }
    }
}