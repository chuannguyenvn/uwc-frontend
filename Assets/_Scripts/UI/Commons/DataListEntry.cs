using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Commons
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
            PrimaryText.AddToClassList("primary-text");
            TextContainer.Add(PrimaryText);

            SecondaryText = new TextElement();
            SecondaryText.AddToClassList("secondary-text");
            TextContainer.Add(SecondaryText);
        }
    }
}