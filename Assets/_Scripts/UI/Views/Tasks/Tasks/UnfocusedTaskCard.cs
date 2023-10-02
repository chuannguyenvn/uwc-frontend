using System;
using Commons.Types;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class UnfocusedTaskCard : View
    {
        public VisualElement ContentContainer;
        public TextElement AddressText;

        public UnfocusedTaskCard(McpFillStatus mcpFillStatus) : base(nameof(UnfocusedTaskCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/UnfocusedTaskCard"));
            AddToClassList("task-card");

            ContentContainer = new VisualElement { name = "ContentContainer" };
            Add(ContentContainer);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.text = "Address";
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("white-text");
            ContentContainer.Add(AddressText);

            switch (mcpFillStatus)
            {
                case McpFillStatus.Full:
                    ContentContainer.AddToClassList("full");
                    break;
                case McpFillStatus.AlmostFull:
                    ContentContainer.AddToClassList("almost-full");
                    break;
                case McpFillStatus.NotFull:
                    ContentContainer.AddToClassList("not-full");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mcpFillStatus), mcpFillStatus, null);
            }
        }
    }
}