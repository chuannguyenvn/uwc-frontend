using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Messaging.Inbox
{
    public class InboxContainer : AdaptiveElement
    {
        private InboxHeader _inboxHeader;
        private MessageList _messageList;
        private InputBar _inputBar;
        
        public InboxContainer() : base(nameof(InboxContainer))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Messaging/Inbox/InboxContainer"));
            AddToClassList("full-view");
            
            _inboxHeader = new InboxHeader();
            Add(_inboxHeader);
            
            _messageList = new MessageList();
            Add(_messageList);
            
            _inputBar = new InputBar();
            Add(_inputBar);
        }
    }
}