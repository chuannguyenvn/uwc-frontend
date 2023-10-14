using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxContainer : AdaptiveElement
    {
        public InboxHeader InboxHeader;
        public MessageList MessageList;
        public InputBar InputBar;
        
        public InboxContainer() : base(nameof(InboxContainer))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/InboxContainer"));
            AddToClassList("full-view");
            
            InboxHeader = new InboxHeader();
            Add(InboxHeader);
            
            MessageList = new MessageList();
            Add(MessageList);
            
            InputBar = new InputBar();
            Add(InputBar);
        }
    }
}