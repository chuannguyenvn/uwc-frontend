using Requests;
using Settings;
using UI.Base;
using UI.Reusables.ChatBubbles;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InputBar : AdaptiveElement
    {
        private TextField _textField;
        private Button _sendButton;
        private VisualElement _sendIcon;

        public InputBar() : base(nameof(InputBar))
        {
            ConfigureUss(nameof(InputBar));

            CreateTextField();
            CreateSendButton();
        }

        private void CreateTextField()
        {
            _textField = new TextField { name = "TextField" };
            _textField.AddToClassList("normal-text");
            _textField.AddToClassList("black-text");
            Add(_textField);

            // _textField.RegisterCallback<FocusInEvent>(_ => { GetFirstAncestorOfType<Root>().ShowKeyboard(); });
            // _textField.RegisterCallback<FocusOutEvent>(_ => { GetFirstAncestorOfType<Root>().HideKeyboard(); });

            _textField.RegisterCallback<KeyDownEvent>(e =>
            {
                if (e.keyCode == KeyCode.Return) SendMessage();
            });
        }

        private void CreateSendButton()
        {
            _sendButton = new Button { name = "SendButton" };
            Add(_sendButton);

            _sendIcon = new VisualElement { name = "SendIcon" };
            _sendButton.Add(_sendIcon);

            _sendButton.RegisterCallback<ClickEvent>(_ => { SendMessage(); });
        }

        private void SendMessage()
        {
            DataStoreManager.Messaging.InboxMessageList.SendMessage(_textField.value);
            _textField.value = "";
            if (Configs.IS_DESKTOP)
                GetFirstAncestorOfType<Root>().Q<ChatBubblesPanel>().FocusInbox(DataStoreManager.Messaging.InboxMessageList.OtherUserProfile);
        }
    }
}