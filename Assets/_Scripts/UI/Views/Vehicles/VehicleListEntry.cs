using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class VehicleListEntry : AdaptiveElement
    {
        public Image Image;

        public VisualElement TextContainer;
        public TextElement LicensePlateText;
        public TextElement ClassText;

        public VehicleListEntry(VehicleData vehicleData) : base(nameof(VehicleListEntry))
        {
            AddToClassList("list-entry");

            Image = new Image { name = "Avatar" };
            Add(Image);

            TextContainer = new VisualElement { name = "TextContainer" };
            Add(TextContainer);

            LicensePlateText = new TextElement { name = "LicensePlateText" };
            LicensePlateText.AddToClassList("normal-text");
            LicensePlateText.AddToClassList("black-text");
            LicensePlateText.text = vehicleData.LicensePlate;
            TextContainer.Add(LicensePlateText);

            ClassText = new TextElement { name = "ClassText" };
            ClassText.AddToClassList("sub-text");
            ClassText.AddToClassList("grey-text");
            ClassText.text = vehicleData.VehicleType.ToString();
            TextContainer.Add(ClassText);
        }
    }
}