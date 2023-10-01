using System;
using Commons.Types;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class TaskEntry : VisualElement
    {
        private readonly VisualElement _status;
        private readonly TextElement _statusText;
        private readonly VisualElement _upperLine;
        private readonly VisualElement _lowerLine;

        private readonly VisualElement _card;
        private readonly TextElement _addressText;
        private readonly VisualElement _crossLine;

        public TaskEntry()
        {
            AddToClassList("task-entry");

            _status = new VisualElement { name = "Status" };
            _status.AddToClassList("status");
            Add(_status);

            _upperLine = new VisualElement { name = "UpperLine" };
            _upperLine.AddToClassList("upper-line");
            _upperLine.AddToClassList("timeline-line");
            _status.Add(_upperLine);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.text = "Status";
            _statusText.AddToClassList("status-text");
            _statusText.AddToClassList("normal-text");
            _statusText.AddToClassList("black-text");
            _status.Add(_statusText);

            _lowerLine = new VisualElement { name = "LowerLine" };
            _lowerLine.AddToClassList("lower-line");
            _lowerLine.AddToClassList("timeline-line");
            _status.Add(_lowerLine);

            _card = new VisualElement { name = "Card" };
            _card.AddToClassList("card");
            Add(_card);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.text = "Address placeholder";
            _addressText.AddToClassList("address-text");
            _addressText.AddToClassList("normal-text");
            _card.Add(_addressText);

            _crossLine = new VisualElement { name = "CrossLine" };
            _crossLine.AddToClassList("cross-line");
            _card.Add(_crossLine);
        }

        public void SetFillStatus(McpFillStatus status)
        {
            switch (status)
            {
                case McpFillStatus.Full:
                    _card.AddToClassList("mcp-full");
                    break;
                case McpFillStatus.AlmostFull:
                    _card.AddToClassList("mcp-almost-full");
                    break;
                case McpFillStatus.NotFull:
                    _card.AddToClassList("mcp-not-full");
                    break;
            }
        }

        public void SetCompletionStatus(bool isCompleted)
        {
            if (isCompleted)
            {
                _addressText.AddToClassList("grey-text");
            }
            else
            {
                _addressText.AddToClassList("white-text");
            }
        }
        
        public void SetFocusStatus(bool isFocused)
        {
            if (isFocused)
            {
                _card.AddToClassList("focused");
            }
            else
            {
                _card.RemoveFromClassList("focused");
            }
        }
    }
}