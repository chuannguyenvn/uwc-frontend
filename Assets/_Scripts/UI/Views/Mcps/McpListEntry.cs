using System;
using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;
using Utilities;
using Random = UnityEngine.Random;

namespace UI.Views.Mcps
{
    public class McpListEntry : AdaptiveElement
    {
        // Information
        private VisualElement _informationContainer;
        private VisualElement _iconContainer;
        private Image _icon;
        private VisualElement _textContainer;
        private TextElement _addressText;
        private TextElement _currentLoadPercentageText;

        // Logs
        private VisualElement _logsContainer;

        public McpListEntry(McpData mcpData, float currentLoadPercentage) : base(nameof(McpListEntry))
        {
            ConfigureUss(nameof(McpListEntry));

            CreateInformation(mcpData, currentLoadPercentage);
            CreateLogs();
        }

        private void CreateInformation(McpData mcpData, float currentLoadPercentage)
        {
            _informationContainer = new VisualElement { name = "InformationContainer" };
            Add(_informationContainer);

            CreateIcon(currentLoadPercentage);
            CreateMcpName(mcpData, currentLoadPercentage);
        }

        private void CreateMcpName(McpData mcpData, float currentLoadPercentage)
        {
            _textContainer = new VisualElement { name = "TextContainer" };
            _informationContainer.Add(_textContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList("black-text");
            _addressText.text = Utility.RemoveDiacritics(mcpData.Address);
            _textContainer.Add(_addressText);

            _currentLoadPercentageText = new TextElement { name = "CurrentLoadPercentageText" };
            _currentLoadPercentageText.AddToClassList("sub-text");
            _currentLoadPercentageText.AddToClassList("grey-text");
            _currentLoadPercentageText.text = currentLoadPercentage.ToString("F2") + "%";
            _textContainer.Add(_currentLoadPercentageText);
        }

        private void CreateIcon(float currentLoadPercentage)
        {
            _iconContainer = new VisualElement { name = "IconContainer" };
            _informationContainer.Add(_iconContainer);

            _icon = new Image { name = "Icon" };
            _iconContainer.Add(_icon);

            if (currentLoadPercentage < 90)
            {
                _iconContainer.AddToClassList("not-full");
            }
            else if (currentLoadPercentage < 100)
            {
                _iconContainer.AddToClassList("almost-full");
            }
            else
            {
                _iconContainer.AddToClassList("full");
            }
        }

        private void CreateLogs()
        {
            _logsContainer = new VisualElement { name = "LogsContainer" };
            Add(_logsContainer);

            for (int i = 0; i < Random.Range(1, 8); i++)
            {
                _logsContainer.Add(new LogEntry(DateTime.Now.AddDays(i).AddHours(Random.Range(0, 10)).AddMinutes(Random.Range(0, 10) * 15)));
            }
        }
    }
}