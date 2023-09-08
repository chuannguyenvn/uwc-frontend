using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Messages
{
    public class MessagingField : VisualElement
    {
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

        private TextField _textField;
        private Button _sendButton;

        public MessagingField()
        {
            name = "MessagingField";

            _textField = new TextField() { name = "TextField" };
            Add(_textField);

            _sendButton = new Button() { name = "SendButton" };
            Add(_sendButton);
        }
    }
}