using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class VehicleInformationPanel : Panel
    {
        private VisualElement _licensePlateContainer;
        private TextElement _licensePlateTitleText;
        private TextElement _licensePlateText;

        private VisualElement _classAndModelContainer;
        private VisualElement _classContainer;
        private TextElement _classTitleText;
        private TextElement _classText;

        private VisualElement _modelContainer;
        private TextElement _modelTitleText;
        private TextElement _modelText;


        public VehicleInformationPanel() : base(nameof(VehicleInformationPanel))
        {
            ConfigureUss(nameof(VehicleInformationPanel));

            AddToClassList("rounded-32px");

            CreateLicensePlate();
            CreateClassAndModel();
        }

        private void CreateLicensePlate()
        {
            _licensePlateContainer = new VisualElement { name = "LicensePlateContainer" };
            Add(_licensePlateContainer);

            _licensePlateTitleText = new TextElement { name = "RoleText" };
            _licensePlateTitleText.AddToClassList("sub-text");
            _licensePlateTitleText.AddToClassList("grey-text");
            _licensePlateTitleText.text = "Vehicle";
            _licensePlateContainer.Add(_licensePlateTitleText);

            _licensePlateText = new TextElement { name = "NameText" };
            _licensePlateText.AddToClassList("normal-text");
            _licensePlateText.AddToClassList("black-text");
            _licensePlateText.text = "51F-3R4-5T6";
            _licensePlateContainer.Add(_licensePlateText);
        }

        private void CreateClassAndModel()
        {
            _classAndModelContainer = new VisualElement { name = "ClassAndModelContainer" };
            Add(_classAndModelContainer);

            CreateClass();
            CreateModel();
        }

        private void CreateClass()
        {
            _classContainer = new VisualElement { name = "ClassContainer" };
            _classAndModelContainer.Add(_classContainer);

            _classTitleText = new TextElement { name = "ClassTitleText" };
            _classTitleText.AddToClassList("sub-text");
            _classTitleText.AddToClassList("grey-text");
            _classTitleText.text = "Class";
            _classContainer.Add(_classTitleText);

            _classText = new TextElement { name = "ClassText" };
            _classText.AddToClassList("normal-text");
            _classText.AddToClassList("black-text");
            _classText.text = "Sideloader";
            _classContainer.Add(_classText);
        }

        private void CreateModel()
        {
            _modelContainer = new VisualElement { name = "ModelContainer" };
            _classAndModelContainer.Add(_modelContainer);

            _modelTitleText = new TextElement { name = "ModelTitleText" };
            _modelTitleText.AddToClassList("sub-text");
            _modelTitleText.AddToClassList("grey-text");
            _modelTitleText.text = "Model";
            _modelContainer.Add(_modelTitleText);

            _modelText = new TextElement { name = "ModelText" };
            _modelText.AddToClassList("normal-text");
            _modelText.AddToClassList("black-text");
            _modelText.text = "LMAO-420";
            _modelContainer.Add(_modelText);
        }
    }
}