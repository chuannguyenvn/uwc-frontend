using Requests;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InputBar : AdaptiveElement
    {
        public TextField TextField;
        public Button SendButton;
        public VisualElement SendIcon;

        public InputBar() : base(nameof(InputBar))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/InputBar"));

            TextField = new TextField { name = "TextField" };
            TextField.AddToClassList("normal-text");
            TextField.AddToClassList("black-text");
            Add(TextField);
            // TextField.RegisterCallback<FocusInEvent>(_ => { GetFirstAncestorOfType<Root>().ShowKeyboard(); });
            // TextField.RegisterCallback<FocusOutEvent>(_ => { GetFirstAncestorOfType<Root>().HideKeyboard(); });

            SendButton = new Button { name = "SendButton" };
            Add(SendButton);

            SendIcon = new VisualElement { name = "SendIcon" };
            SendButton.Add(SendIcon);

            SendButton.RegisterCallback<ClickEvent>(_ => { SendMessage(); });
        }

        private void SendMessage()
        {
            DataStoreManager.Messaging.InboxMessageList.SendMessage(TextField.value);
            TextField.value = "";
        }
    }
}