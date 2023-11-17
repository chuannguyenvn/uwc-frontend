using System;
using Commons.Models;
using Commons.Types;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Tasks
{
    public class FocusedTaskCard : TaskCard
    {
        private readonly TaskData _taskData;

        // Panel
        private VisualElement _contentContainer;
        private VisualElement _panel;
        private VisualElement _mask;

        // Address
        private VisualElement _addressContainer;
        private TextElement _addressText;

        // Details
        private VisualElement _detailsContainer;
        private VisualElement _currentLoadContainer;
        private TextElement _currentLoadTitleText;
        private TextElement _currentLoadValueText;
        private VisualElement _etaContainer;
        private TextElement _etaTitleText;
        private TextElement _etaValueText;

        public FocusedTaskCard(TaskData taskData, McpFillStatus mcpFillStatus) : base(nameof(FocusedTaskCard))
        {
            _taskData = taskData;

            ConfigureUss(nameof(FocusedTaskCard));

            CreateMask();
            CreateAddress();
            CreateDetails();
            ModifyBasedOnFillStatus(mcpFillStatus);
        }

        private void CreateMask()
        {
            _contentContainer = new VisualElement { name = "ContentContainer" };
            Add(_contentContainer);

            _panel = new VisualElement { name = "Panel" };
            _contentContainer.Add(_panel);

            _mask = new VisualElement { name = "Mask" };
            _contentContainer.Add(_mask);
        }

        private void CreateAddress()
        {
            _addressContainer = new VisualElement { name = "AddressContainer" };
            _addressContainer.AddToClassList("sub-container");
            _mask.Add(_addressContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.text = _taskData.McpData.Address;
            _addressText.AddToClassList("title-text");
            _addressText.AddToClassList("white-text");
            _addressContainer.Add(_addressText);
        }

        private void CreateDetails()
        {
            _detailsContainer = new VisualElement { name = "DetailsContainer" };
            _detailsContainer.AddToClassList("sub-container");
            _mask.Add(_detailsContainer);

            CreateCurrentLoad();
            CreateEta();
        }

        private void CreateCurrentLoad()
        {
            _currentLoadContainer = new VisualElement { name = "CurrentLoadContainer" };
            _detailsContainer.Add(_currentLoadContainer);

            _currentLoadTitleText = new TextElement { name = "CurrentLoadTitleText" };
            _currentLoadTitleText.text = "Current load:";
            _currentLoadTitleText.AddToClassList("normal-text");
            _currentLoadTitleText.AddToClassList("black-text");
            _currentLoadContainer.Add(_currentLoadTitleText);

            _currentLoadValueText = new TextElement { name = "CurrentLoadText" };
            _currentLoadValueText.text = "90%";
            _currentLoadValueText.AddToClassList("title-text");
            _currentLoadValueText.AddToClassList("black-text");
            _currentLoadContainer.Add(_currentLoadValueText);
        }

        private void CreateEta()
        {
            _etaContainer = new VisualElement { name = "EtaContainer" };
            _detailsContainer.Add(_etaContainer);

            _etaTitleText = new TextElement { name = "EtaTitleText" };
            _etaTitleText.text = "ETA:";
            _etaTitleText.AddToClassList("normal-text");
            _etaTitleText.AddToClassList("black-text");
            _etaContainer.Add(_etaTitleText);

            _etaValueText = new TextElement { name = "EtaValueText" };
            _etaValueText.text = "10:05AM";
            _etaValueText.AddToClassList("title-text");
            _etaValueText.AddToClassList("black-text");
            _etaContainer.Add(_etaValueText);
        }

        private void ModifyBasedOnFillStatus(McpFillStatus mcpFillStatus)
        {
            switch (mcpFillStatus)
            {
                case McpFillStatus.Full:
                    _addressContainer.AddToClassList("full");
                    break;
                case McpFillStatus.AlmostFull:
                    _addressContainer.AddToClassList("almost-full");
                    break;
                case McpFillStatus.NotFull:
                    _addressContainer.AddToClassList("not-full");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mcpFillStatus), mcpFillStatus, null);
            }
        }
    }
}