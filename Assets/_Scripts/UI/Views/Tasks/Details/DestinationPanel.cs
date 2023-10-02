using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class DestinationPanel : Panel
    {
        public VisualElement AddressContainer;
        public TextElement AddressTitle;
        public TextElement AddressText;

        public VisualElement DistanceAndEtaContainer;
        public VisualElement DistanceContainer;
        public TextElement DistanceTitle;
        public TextElement DistanceText;
        public VisualElement EtaContainer;
        public TextElement EtaTitle;
        public TextElement EtaText;

        public DestinationPanel() : base(nameof(DestinationPanel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/DestinationPanel"));
            AddToClassList("rounded-32px");
            CreateAddress();
            CreateDistanceAndEta();
        }

        private void CreateAddress()
        {
            AddressContainer = new VisualElement { name = "AddressContainer" };
            Add(AddressContainer);

            AddressTitle = new TextElement { name = "AddressTitle" };
            AddressTitle.AddToClassList("sub-text");
            AddressTitle.AddToClassList("grey-text");
            AddressTitle.text = "Address";
            AddressContainer.Add(AddressTitle);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.AddToClassList("normal-text");
            AddressText.AddToClassList("black-text");
            AddressText.text = "Address placeholder";
            AddressContainer.Add(AddressText);
        }

        private void CreateDistanceAndEta()
        {
            DistanceAndEtaContainer = new VisualElement { name = "DistanceAndEtaContainer" };
            Add(DistanceAndEtaContainer);

            CreateDistance();
            CreateEta();
        }

        private void CreateDistance()
        {
            DistanceContainer = new VisualElement { name = "DistanceContainer" };
            DistanceAndEtaContainer.Add(DistanceContainer);

            DistanceTitle = new TextElement { name = "DistanceTitle" };
            DistanceTitle.AddToClassList("sub-text");
            DistanceTitle.AddToClassList("grey-text");
            DistanceTitle.text = "Distance";
            DistanceContainer.Add(DistanceTitle);

            DistanceText = new TextElement { name = "DistanceText" };
            DistanceText.AddToClassList("normal-text");
            DistanceText.AddToClassList("black-text");
            DistanceText.text = "Distance placeholder";
            DistanceContainer.Add(DistanceText);
        }

        private void CreateEta()
        {
            EtaContainer = new VisualElement { name = "EtaContainer" };
            DistanceAndEtaContainer.Add(EtaContainer);

            EtaTitle = new TextElement { name = "EtaTitle" };
            EtaTitle.AddToClassList("sub-text");
            EtaTitle.AddToClassList("grey-text");
            EtaTitle.text = "ETA";
            EtaContainer.Add(EtaTitle);

            EtaText = new TextElement { name = "EtaText" };
            EtaText.AddToClassList("normal-text");
            EtaText.AddToClassList("black-text");
            EtaText.text = "ETA placeholder";
            EtaContainer.Add(EtaText);
        }
    }
}