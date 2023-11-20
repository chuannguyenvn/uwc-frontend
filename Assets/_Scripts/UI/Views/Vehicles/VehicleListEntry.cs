using System;
using Commons.Categories;
using Commons.Models;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Vehicles
{
    public class VehicleListEntry : AnimatedButton
    {
        public VehicleData VehicleData { get; }
        public event Action Clicked;

        // Image
        private Image _image;

        // Details
        private VisualElement _textContainer;
        private TextElement _licensePlateText;
        private TextElement _classText;

        public VehicleListEntry(VehicleData vehicleData) : base(nameof(VehicleListEntry))
        {
            VehicleData = vehicleData;

            ConfigureUss(nameof(VehicleListEntry));

            AddToClassList("white-button");
            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");

            // CreateImage();
            CreateDetails(vehicleData);

            RegisterCallback<ClickEvent>(_ => Clicked?.Invoke());
        }

        private void CreateImage()
        {
            _image = new Image { name = "Avatar" };
            Add(_image);
        }

        private void CreateDetails(VehicleData vehicleData)
        {
            _textContainer = new VisualElement { name = "TextContainer" };
            Add(_textContainer);

            _licensePlateText = new TextElement { name = "LicensePlateText" };
            _licensePlateText.AddToClassList("normal-text");
            _licensePlateText.AddToClassList("black-text");
            _licensePlateText.text = vehicleData.LicensePlate;
            _textContainer.Add(_licensePlateText);

            _classText = new TextElement { name = "ClassText" };
            _classText.AddToClassList("sub-text");
            _classText.AddToClassList("grey-text");
            _classText.text = vehicleData.VehicleType.GetFriendlyString();
            _textContainer.Add(_classText);
        }
    }
}