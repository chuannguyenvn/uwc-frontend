using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class McpListEntry : AdaptiveElement
    {
        public Image Image;

        public VisualElement TextContainer;
        public TextElement AddressText;
        public TextElement CurrentLoadPercentageText;

        public McpListEntry(McpData mcpData, float currentLoadPercentage) : base(nameof(McpListEntry))
        {
            AddToClassList("list-entry");

            Image = new Image { name = "Avatar" };
            Add(Image);

            TextContainer = new VisualElement { name = "TextContainer" };
            Add(TextContainer);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("black-text");
            AddressText.text = mcpData.Address;
            TextContainer.Add(AddressText);

            CurrentLoadPercentageText = new TextElement { name = "CurrentLoadPercentageText" };
            CurrentLoadPercentageText.AddToClassList("sub-text");
            CurrentLoadPercentageText.AddToClassList("grey-text");
            CurrentLoadPercentageText.text = currentLoadPercentage.ToString("F2") + "%";
            TextContainer.Add(CurrentLoadPercentageText);
        }
    }
}