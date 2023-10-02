using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Status
{
    public class VehicleInformationPanel : Panel
    {
        public VisualElement LicensePlateContainer;
        public TextElement LicensePlateTitleText;
        public TextElement LicensePlateText;

        public VisualElement ClassAndModelContainer;
        public VisualElement ClassContainer;
        public TextElement ClassTitleText;
        public TextElement ClassText;

        public VisualElement ModelContainer;
        public TextElement ModelTitleText;
        public TextElement ModelText;


        public VehicleInformationPanel() : base(nameof(VehicleInformationPanel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Status/VehicleInformationPanel"));
            AddToClassList("rounded-32px");

            CreateLicensePlate();
            CreateClassAndModel();
        }

        private void CreateLicensePlate()
        {
            LicensePlateContainer = new VisualElement { name = "LicensePlateContainer" };
            Add(LicensePlateContainer);

            LicensePlateTitleText = new TextElement { name = "RoleText" };
            LicensePlateTitleText.AddToClassList("sub-text");
            LicensePlateTitleText.AddToClassList("grey-text");
            LicensePlateTitleText.text = "Vehicle";
            LicensePlateContainer.Add(LicensePlateTitleText);

            LicensePlateText = new TextElement { name = "NameText" };
            LicensePlateText.AddToClassList("normal-text");
            LicensePlateText.AddToClassList("black-text");
            LicensePlateText.text = "51F-3R4-5T6";
            LicensePlateContainer.Add(LicensePlateText);
        }

        private void CreateClassAndModel()
        {
            ClassAndModelContainer = new VisualElement { name = "ClassAndModelContainer" };
            Add(ClassAndModelContainer);

            CreateClass();
            CreateModel();
        }

        private void CreateClass()
        {
            ClassContainer = new VisualElement { name = "ClassContainer" };
            ClassAndModelContainer.Add(ClassContainer);

            ClassTitleText = new TextElement { name = "ClassTitleText" };
            ClassTitleText.AddToClassList("sub-text");
            ClassTitleText.AddToClassList("grey-text");
            ClassTitleText.text = "Class";
            ClassContainer.Add(ClassTitleText);

            ClassText = new TextElement { name = "ClassText" };
            ClassText.AddToClassList("normal-text");
            ClassText.AddToClassList("black-text");
            ClassText.text = "Sideloader";
            ClassContainer.Add(ClassText);
        }
        
        private void CreateModel()
        {
            ModelContainer = new VisualElement { name = "ModelContainer" };
            ClassAndModelContainer.Add(ModelContainer);

            ModelTitleText = new TextElement { name = "ModelTitleText" };
            ModelTitleText.AddToClassList("sub-text");
            ModelTitleText.AddToClassList("grey-text");
            ModelTitleText.text = "Model";
            ModelContainer.Add(ModelTitleText);

            ModelText = new TextElement { name = "ModelText" };
            ModelText.AddToClassList("normal-text");
            ModelText.AddToClassList("black-text");
            ModelText.text = "LMAO-420";
            ModelContainer.Add(ModelText);
        }
    }
}