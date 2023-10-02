using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InputBar : AdaptiveElement
    {
        private TextField _textField;
        private Button _sendButton;

        public InputBar() : base(nameof(InputBar))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/InputBar"));

            _textField = new TextField { name = "TextField" };
            _textField.AddToClassList("normal-text");
            _textField.AddToClassList("black-text");
            Add(_textField);

            _sendButton = new Button { name = "SendButton" };
            Add(_sendButton);
        }
    }
}