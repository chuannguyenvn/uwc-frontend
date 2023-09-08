using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class MessagingField : VisualElement
    {
        private readonly Button _sendButton;
        private readonly Image _sendIcon;

        private readonly TextField _textField;

        public MessagingField()
        {
            name = "MessagingField";

            _textField = new TextField { name = "TextField" };
            Add(_textField);

            _sendButton = new Button { name = "SendButton" };
            _sendButton.AddToClassList("colored-element");
            Add(_sendButton);

            _sendIcon = new Image { name = "SendIcon" };
            _sendIcon.AddToClassList("icon");
            _sendButton.Add(_sendIcon);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<MessagingField, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}