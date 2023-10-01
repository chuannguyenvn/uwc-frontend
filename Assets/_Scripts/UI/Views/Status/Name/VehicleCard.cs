using UI.Common;
using UnityEngine.UIElements;

namespace UI.Views.Status.Name
{
    public class VehicleCard : Card
    {
        private readonly VisualElement _licensePlateContainer;
        private readonly TextElement _vehicleTitleText;
        private readonly TextElement _licensePlateText;

        private readonly VisualElement _vehicleDetailsContainer;
        private readonly VisualElement _classContainer;
        private readonly TextElement _classTitleText;
        private readonly TextElement _classText;
        private readonly VisualElement _modelContainer;
        private readonly TextElement _modelTitleText;
        private readonly TextElement _modelText;

        public VehicleCard() : base("Vehicle")
        {
            _licensePlateContainer = new VisualElement { name = "LicensePlateContainer" };
            _licensePlateContainer.AddToClassList("license-plate-container");
            Add(_licensePlateContainer);

            _vehicleTitleText = new TextElement { name = "Role" };
            _vehicleTitleText.text = "Vehicle";
            _vehicleTitleText.AddToClassList("role");
            _vehicleTitleText.AddToClassList("sub-text");
            _vehicleTitleText.AddToClassList("grey-text");
            _licensePlateContainer.Add(_vehicleTitleText);

            _licensePlateText = new TextElement { name = "Name" };
            _licensePlateText.text = "51F-25764";
            _licensePlateText.AddToClassList("name");
            _licensePlateText.AddToClassList("title-text");
            _licensePlateText.AddToClassList("black-text");
            _licensePlateContainer.Add(_licensePlateText);

            var separator = new VisualElement { name = "Separator" };
            separator.AddToClassList("separator");
            Add(separator);
            
            _vehicleDetailsContainer = new VisualElement { name = "VehicleDetailsContainer" };
            _vehicleDetailsContainer.AddToClassList("vehicle-details-container");
            Add(_vehicleDetailsContainer);

            _classContainer = new VisualElement { name = "ClassContainer" };
            _classContainer.AddToClassList("class-container");
            _vehicleDetailsContainer.Add(_classContainer);

            _classTitleText = new TextElement { name = "ClassTitle" };
            _classTitleText.text = "Class";
            _classTitleText.AddToClassList("class-title");
            _classTitleText.AddToClassList("sub-text");
            _classTitleText.AddToClassList("grey-text");
            _classContainer.Add(_classTitleText);

            _classText = new TextElement { name = "Class" };
            _classText.text = "Sideloader";
            _classText.AddToClassList("class");
            _classText.AddToClassList("title-text");
            _classText.AddToClassList("black-text");
            _classContainer.Add(_classText);

            _modelContainer = new VisualElement { name = "ModelContainer" };
            _modelContainer.AddToClassList("model-container");
            _vehicleDetailsContainer.Add(_modelContainer);

            _modelTitleText = new TextElement { name = "ModelTitle" };
            _modelTitleText.text = "Model";
            _modelTitleText.AddToClassList("model-title");
            _modelTitleText.AddToClassList("sub-text");
            _modelTitleText.AddToClassList("grey-text");
            _modelContainer.Add(_modelTitleText);

            _modelText = new TextElement { name = "Model" };
            _modelText.text = "LMAO-420";
            _modelText.AddToClassList("model");
            _modelText.AddToClassList("title-text");
            _modelText.AddToClassList("black-text");
            _modelContainer.Add(_modelText);
        }
    }
}