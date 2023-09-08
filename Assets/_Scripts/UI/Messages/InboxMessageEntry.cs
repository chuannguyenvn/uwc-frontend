using System;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Messages
{
    public class InboxMessageEntry : VisualElement
    {
        private readonly TextElement _content;

        private readonly VisualElement _contentContainer;
        private readonly TextElement _timestamp;

        public InboxMessageEntry()
        {
            name = "InboxMessageEntry";

            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _content = new TextElement { name = "Content" };
            _contentContainer.Add(_content);

            _timestamp = new TextElement { name = "Timestamp" };
            Add(_timestamp);

            SetData("Test", DateTime.Now, true);
        }

        public InboxMessageEntry(string content, DateTime timestamp, bool isFromUser)
        {
            name = "InboxMessageEntry";

            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _content = new TextElement { name = "Content" };
            _contentContainer.Add(_content);

            _timestamp = new TextElement { name = "Timestamp" };
            Add(_timestamp);

            SetData(content, timestamp, isFromUser);
        }

        public void SetData(string content, DateTime timestamp, bool isFromUser)
        {
            _content.text = content;
            _content.EnableInClassList("white-text", isFromUser);
            _content.EnableInClassList("black-text", !isFromUser);

            _timestamp.text = timestamp.ToString("dd/MM/yyyy HH:mm");

            style.alignSelf = _contentContainer.style.alignSelf = _timestamp.style.alignSelf = isFromUser ? Align.FlexEnd : Align.FlexStart;
            _contentContainer.EnableInClassList("colored-element", isFromUser);
            _contentContainer.EnableInClassList("grey-element", !isFromUser);
        }

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
    }
}