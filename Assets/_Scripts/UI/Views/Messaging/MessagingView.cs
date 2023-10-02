using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging
{
    public class MessagingView : View
    {
        public MessagingView() : base(nameof(MessagingView), true)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/UI/Views/Messaging/MessagingView"));
        }
    }
}