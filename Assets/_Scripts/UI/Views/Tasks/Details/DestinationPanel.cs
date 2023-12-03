using LocalizationNS;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class DestinationPanel : Panel
    {
        private VisualElement _addressContainer;
        private TextElement _addressTitle;
        private TextElement _addressText;

        private VisualElement _distanceAndEtaContainer;
        private VisualElement _distanceContainer;
        private TextElement _distanceTitle;
        private TextElement _distanceText;
        private VisualElement _etaContainer;
        private TextElement _etaTitle;
        private TextElement _etaText;

        public DestinationPanel() : base(nameof(DestinationPanel))
        {
            ConfigureUss(nameof(DestinationPanel));

            AddToClassList("rounded-32px");

            CreateAddress();
            CreateDistanceAndEta();
        }

        private void CreateAddress()
        {
            _addressContainer = new VisualElement { name = "AddressContainer" };
            Add(_addressContainer);

            _addressTitle = new TextElement { name = "AddressTitle" };
            _addressTitle.AddToClassList("sub-text");
            _addressTitle.AddToClassList("grey-text");
            _addressTitle.text = Localization.GetSentence(Sentence.TasksView.ADDRESS);
            _addressContainer.Add(_addressTitle);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList("black-text");
            _addressText.text = "Address placeholder";
            _addressContainer.Add(_addressText);
        }

        private void CreateDistanceAndEta()
        {
            _distanceAndEtaContainer = new VisualElement { name = "DistanceAndEtaContainer" };
            Add(_distanceAndEtaContainer);

            CreateDistance();
            CreateEta();
        }

        private void CreateDistance()
        {
            _distanceContainer = new VisualElement { name = "DistanceContainer" };
            _distanceAndEtaContainer.Add(_distanceContainer);

            _distanceTitle = new TextElement { name = "DistanceTitle" };
            _distanceTitle.AddToClassList("sub-text");
            _distanceTitle.AddToClassList("grey-text");
            _distanceTitle.text = Localization.GetSentence(Sentence.TasksView.DISTANCE);
            _distanceContainer.Add(_distanceTitle);

            _distanceText = new TextElement { name = "DistanceText" };
            _distanceText.AddToClassList("normal-text");
            _distanceText.AddToClassList("black-text");
            _distanceText.text = "Distance placeholder";
            _distanceContainer.Add(_distanceText);
        }

        private void CreateEta()
        {
            _etaContainer = new VisualElement { name = "EtaContainer" };
            _distanceAndEtaContainer.Add(_etaContainer);

            _etaTitle = new TextElement { name = "EtaTitle" };
            _etaTitle.AddToClassList("sub-text");
            _etaTitle.AddToClassList("grey-text");
            _etaTitle.text = Localization.GetSentence(Sentence.TasksView.ETA);
            _etaContainer.Add(_etaTitle);

            _etaText = new TextElement { name = "EtaText" };
            _etaText.AddToClassList("normal-text");
            _etaText.AddToClassList("black-text");
            _etaText.text = "ETA placeholder";
            _etaContainer.Add(_etaText);
        }
        
        public void SetAddressText(string address)
        {
            _addressText.text = address;
        }
    }
}