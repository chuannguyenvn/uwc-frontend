using System;
using Commons.Types;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class FocusedTaskCard : View
    {
        public VisualElement ContentContainer;
        public VisualElement AddressContainer;
        public TextElement AddressText;
        public VisualElement DetailsContainer;
        public TextElement CurrentLoadText;
        public TextElement EtaText;

        public FocusedTaskCard(McpFillStatus mcpFillStatus) : base(nameof(FocusedTaskCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/FocusedTaskCard"));
            AddToClassList("task-card");

            ContentContainer = new VisualElement { name = "ContentContainer" };
            Add(ContentContainer);

            AddressContainer = new VisualElement { name = "AddressContainer" };
            ContentContainer.Add(AddressContainer);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.text = "Address";
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("white-text");
            AddressContainer.Add(AddressText);

            DetailsContainer = new VisualElement { name = "DetailsContainer" };
            ContentContainer.Add(DetailsContainer);

            CurrentLoadText = new TextElement { name = "CurrentLoadText" };
            CurrentLoadText.text = "69%";
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("black-text");
            DetailsContainer.Add(CurrentLoadText);

            EtaText = new TextElement { name = "EtaText" };
            EtaText.text = "ETA: 4:20";
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("black-text");
            DetailsContainer.Add(EtaText);

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