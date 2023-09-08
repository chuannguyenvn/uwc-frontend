using System;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Messages
{
    public class InboxMessageEntry : VisualElement
    {
        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<InboxMessageEntry, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion

        private VisualElement _contentContainer;
        private TextElement _content;
        private TextElement _timestamp;

        public InboxMessageEntry()
        {
            name = "InboxMessageEntry";

            _contentContainer = new VisualElement() { name = "ContentContainer" };
            Add(_contentContainer);

            _content = new TextElement() { name = "Content" };
            _contentContainer.Add(_content);

            _timestamp = new TextElement() { name = "Timestamp" };
            Add(_timestamp);
            
            SetData("Test", DateTime.Now, true);
        }

        public InboxMessageEntry(string content, DateTime timestamp, bool isFromUser) : base()
        {
            SetData(content, timestamp, isFromUser);
        }

        public void SetData(string content, DateTime timestamp, bool isFromUser)
        {
            _content.text = content;
            _timestamp.text = timestamp.ToString("dd/MM/yyyy HH:mm");
            style.alignSelf = _timestamp.style.alignSelf = isFromUser ? Align.FlexEnd : Align.FlexStart;
        }
    }
}