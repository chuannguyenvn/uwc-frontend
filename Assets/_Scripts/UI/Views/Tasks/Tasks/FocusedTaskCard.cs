using System;
using Commons.Types;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class FocusedTaskCard : View
    {
        public VisualElement ContentContainer;
        public VisualElement Panel;
        public VisualElement Mask;

        public VisualElement AddressContainer;
        public TextElement AddressText;

        public VisualElement DetailsContainer;
        public VisualElement CurrentLoadContainer;
        public TextElement CurrentLoadTitleText;
        public TextElement CurrentLoadValueText;
        public VisualElement EtaContainer;
        public TextElement EtaTitleText;
        public TextElement EtaValueText;

        public FocusedTaskCard(McpFillStatus mcpFillStatus) : base(nameof(FocusedTaskCard))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Tasks/FocusedTaskCard"));
            AddToClassList("task-card");

            CreateMask();
            CreateAddress();
            CreateDetails();

            switch (mcpFillStatus)
            {
                case McpFillStatus.Full:
                    AddressContainer.AddToClassList("full");
                    break;
                case McpFillStatus.AlmostFull:
                    AddressContainer.AddToClassList("almost-full");
                    break;
                case McpFillStatus.NotFull:
                    AddressContainer.AddToClassList("not-full");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mcpFillStatus), mcpFillStatus, null);
            }
        }

        private void CreateMask()
        {
            ContentContainer = new VisualElement { name = "ContentContainer" };
            Add(ContentContainer);

            Panel = new VisualElement { name = "Panel" };
            ContentContainer.Add(Panel);

            Mask = new VisualElement { name = "Mask" };
            ContentContainer.Add(Mask);
        }

        private void CreateAddress()
        {
            AddressContainer = new VisualElement { name = "AddressContainer" };
            AddressContainer.AddToClassList("sub-container");
            Mask.Add(AddressContainer);

            AddressText = new TextElement { name = "AddressText" };
            AddressText.text = "Address";
            AddressText.AddToClassList("title-text");
            AddressText.AddToClassList("white-text");
            AddressContainer.Add(AddressText);
        }

        private void CreateDetails()
        {
            DetailsContainer = new VisualElement { name = "DetailsContainer" };
            DetailsContainer.AddToClassList("sub-container");
            Mask.Add(DetailsContainer);

            CreateCurrentLoad();
            CreateEta();
        }

        private void CreateCurrentLoad()
        {
            CurrentLoadContainer = new VisualElement { name = "CurrentLoadContainer" };
            DetailsContainer.Add(CurrentLoadContainer);
            
            CurrentLoadTitleText = new TextElement { name = "CurrentLoadTitleText" };
            CurrentLoadTitleText.text = "Current load:";
            CurrentLoadTitleText.AddToClassList("normal-text");
            CurrentLoadTitleText.AddToClassList("black-text");
            CurrentLoadContainer.Add(CurrentLoadTitleText);
            
            CurrentLoadValueText = new TextElement { name = "CurrentLoadText" };
            CurrentLoadValueText.text = "90%";
            CurrentLoadValueText.AddToClassList("title-text");
            CurrentLoadValueText.AddToClassList("black-text");
            CurrentLoadContainer.Add(CurrentLoadValueText);
        }

        private void CreateEta()
        {
            EtaContainer = new VisualElement { name = "EtaContainer" };
            DetailsContainer.Add(EtaContainer);
            
            EtaTitleText = new TextElement { name = "EtaTitleText" };
            EtaTitleText.text = "ETA:";
            EtaTitleText.AddToClassList("normal-text");
            EtaTitleText.AddToClassList("black-text");
            EtaContainer.Add(EtaTitleText);
            
            EtaValueText = new TextElement { name = "EtaValueText" };
            EtaValueText.text = "10:05AM";
            EtaValueText.AddToClassList("title-text");
            EtaValueText.AddToClassList("black-text");
            EtaContainer.Add(EtaValueText);
        }
    }
}