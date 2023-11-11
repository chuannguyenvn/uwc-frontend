using System;
using Commons.Models;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class McpListEntry : AdaptiveElement
    {
        public VisualElement InformationContainer;
        public VisualElement AvatarContainer;
        public Image Avatar;
        public VisualElement TextContainer;
        public TextElement AddressText;
        public TextElement CurrentLoadPercentageText;

        public VisualElement LogsContainer;

        public McpListEntry(McpData mcpData, float currentLoadPercentage) : base(nameof(McpListEntry))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Mcps/McpListEntry"));
            AddToClassList("list-entry");

            CreateInformation(mcpData, currentLoadPercentage);
            CreateLogs();
        }

        private void CreateInformation(McpData mcpData, float currentLoadPercentage)
        {
            InformationContainer = new VisualElement { name = "InformationContainer" };
            Add(InformationContainer);

            AvatarContainer = new VisualElement { name = "AvatarContainer" };
            InformationContainer.Add(AvatarContainer);

            Avatar = new Image { name = "Avatar" };
            AvatarContainer.Add(Avatar);

            TextContainer = new VisualElement { name = "TextContainer" };
            InformationContainer.Add(TextContainer);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("black-text");
            AddressText.text = Utility.RemoveDiacritics(mcpData.Address);
            TextContainer.Add(AddressText);

            CurrentLoadPercentageText = new TextElement { name = "CurrentLoadPercentageText" };
            CurrentLoadPercentageText.AddToClassList("sub-text");
            CurrentLoadPercentageText.AddToClassList("grey-text");
            CurrentLoadPercentageText.text = currentLoadPercentage.ToString("F2") + "%";
            TextContainer.Add(CurrentLoadPercentageText);

            if (currentLoadPercentage < 90)
            {
                AvatarContainer.AddToClassList("not-full");
            }
            else if (currentLoadPercentage < 100)
            {
                AvatarContainer.AddToClassList("almost-full");
            }
            else
            {
                AvatarContainer.AddToClassList("full");
            }
        }

        private void CreateLogs()
        {
            LogsContainer = new VisualElement { name = "LogsContainer" };
            Add(LogsContainer);

            for (int i = 0; i < 10; i++)
            {
                LogsContainer.Add(new LogEntry(DateTime.Today));
            }
        }
    }
}