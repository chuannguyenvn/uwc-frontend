using System;
using Commons.Models;
using Commons.Types;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class UnfocusedTaskCard : TaskCard
    {
        private VisualElement _contentContainer;
        private TextElement _addressText;

        public UnfocusedTaskCard(TaskData taskData, McpFillStatus mcpFillStatus) : base(nameof(UnfocusedTaskCard))
        {
            ConfigureUss(nameof(UnfocusedTaskCard));

            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.text = taskData.McpData.Address;
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList("white-text");
            _contentContainer.Add(_addressText);

            switch (mcpFillStatus)
            {
                case McpFillStatus.Full:
                    _contentContainer.AddToClassList("full");
                    break;
                case McpFillStatus.AlmostFull:
                    _contentContainer.AddToClassList("almost-full");
                    break;
                case McpFillStatus.NotFull:
                    _contentContainer.AddToClassList("not-full");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mcpFillStatus), mcpFillStatus, null);
            }
        }
    }
}