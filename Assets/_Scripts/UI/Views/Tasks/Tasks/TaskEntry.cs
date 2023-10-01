using System;
using Commons.Types;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class TaskEntry : VisualElement
    {
        private readonly VisualElement _statusContainer;
        private readonly TextElement _statusText;
        private readonly VisualElement _upperLine;
        private readonly VisualElement _lowerLine;

        private readonly VisualElement _detailsContainer;
        private readonly TextElement _addressText;
        private readonly VisualElement _crossLine;

        public TaskEntry()
        {
            AddToClassList("task-entry");

            _statusContainer = new VisualElement { name = "Status" };
            _statusContainer.AddToClassList("status");
            Add(_statusContainer);

            _upperLine = new VisualElement { name = "UpperLine" };
            _upperLine.AddToClassList("upper-line");
            _upperLine.AddToClassList("timeline-line");
            _statusContainer.Add(_upperLine);

            _statusText = new TextElement { name = "StatusText" };
            _statusText.text = "Status";
            _statusText.AddToClassList("status-text");
            _statusText.AddToClassList("normal-text");
            _statusText.AddToClassList("black-text");
            _statusContainer.Add(_statusText);

            _lowerLine = new VisualElement { name = "LowerLine" };
            _lowerLine.AddToClassList("lower-line");
            _lowerLine.AddToClassList("timeline-line");
            _statusContainer.Add(_lowerLine);

            _detailsContainer = new VisualElement { name = "Card" };
            _detailsContainer.AddToClassList("details");
            Add(_detailsContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.text = "Address placeholder";
            _addressText.AddToClassList("address-text");
            _addressText.AddToClassList("normal-text");
            _detailsContainer.Add(_addressText);

            _crossLine = new VisualElement { name = "CrossLine" };
            _crossLine.AddToClassList("cross-line");
            _detailsContainer.Add(_crossLine);
        }

        public void SetFillStatus(McpFillStatus status)
        {
            switch (status)
            {
                case McpFillStatus.Full:
                    _detailsContainer.AddToClassList("mcp-full");
                    break;
                case McpFillStatus.AlmostFull:
                    _detailsContainer.AddToClassList("mcp-almost-full");
                    break;
                case McpFillStatus.NotFull:
                    _detailsContainer.AddToClassList("mcp-not-full");
                    break;
            }
        }

        public void SetCompletionStatus(bool isCompleted)
        {
            if (isCompleted)
            {
                _addressText.AddToClassList("grey-text");
                _detailsContainer.AddToClassList("grey-background");
                _crossLine.style.display = DisplayStyle.Flex;
            }
            else
            {
                _addressText.AddToClassList("white-text");
                _crossLine.style.display = DisplayStyle.None;
            }
        }
        
        public void SetFocusStatus(bool isFocused)
        {
            if (isFocused)
            {
                _detailsContainer.AddToClassList("focused");
            }
            else
            {
                _detailsContainer.RemoveFromClassList("focused");
            }
        }
    }
}