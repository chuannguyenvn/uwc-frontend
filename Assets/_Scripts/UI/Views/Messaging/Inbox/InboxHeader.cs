using Constants;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxHeader : AdaptiveElement
    {
        private VisualElement _backButton;

        private VisualElement _avatar;

        private VisualElement _textContainer;
        private TextElement _nameText;
        private TextElement _statusText;

        public InboxHeader() : base(nameof(InboxHeader))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/InboxHeader"));

            _backButton = new VisualElement { name = "BackButton" };
            if (!Configs.IS_DESKTOP)
            {
                _backButton.RegisterCallback<MouseUpEvent>(_ =>
                {
                    RegisterCallback<MouseUpEvent>(_ =>
                    {
                        GetFirstAncestorOfType<MessagingView>().InboxContainer.style.display = DisplayStyle.None;
                        GetFirstAncestorOfType<MessagingView>().ContactList.style.display = DisplayStyle.Flex;
                    });
                });
            }
            Add(_backButton);

            _avatar = new VisualElement { name = "Avatar" };
            Add(_avatar);

            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _nameText = new TextElement { name = "NameText" };
            _nameText.AddToClassList("normal-text");
            _nameText.AddToClassList("white-text");
            _nameText.text = "Sender name";
            _textContainer.Add(_nameText);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.AddToClassList("sub-text");
            _statusText.AddToClassList("white-text");
            _statusText.text = "Status";
            _textContainer.Add(_statusText);
        }
    }
}