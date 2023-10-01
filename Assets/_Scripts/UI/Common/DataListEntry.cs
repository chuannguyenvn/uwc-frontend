using UnityEngine.UIElements;

namespace UI.Common
{
    public class DataListEntry : VisualElement
    {
        protected readonly VisualElement ImageContainer;
        protected readonly Image Icon;

        protected readonly VisualElement TextContainer;
        protected readonly TextElement PrimaryText;
        protected readonly TextElement SecondaryText;

        public DataListEntry()
        {
            name = "ContactEntry";
            AddToClassList("list-item");

            ImageContainer = new VisualElement { name = "IconContainer" };
            ImageContainer.AddToClassList("icon-container");
            Add(ImageContainer);
            
            Icon = new Image();
            Icon.AddToClassList("icon");
            ImageContainer.Add(Icon);

            TextContainer = new VisualElement { name = "TextContainer" };
            Add(TextContainer);

            PrimaryText = new TextElement();
            PrimaryText.AddToClassList("normal-text");
            TextContainer.Add(PrimaryText);

            SecondaryText = new TextElement();
            SecondaryText.AddToClassList("sub-text");
            TextContainer.Add(SecondaryText);
        }
    }
}