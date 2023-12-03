using System;
using Commons.Communications.Mcps;
using Commons.Models;
using Maps;
using Requests;
using UI.Base;
using UI.Views.Mcps.AssignTaskProcedure;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace UI.Views.Mcps
{
    public class McpListEntry : AnimatedButton
    {
        public McpData McpData { get; private set; }
        private readonly bool _isTaskAssigning;

        private float _currentLoadPercentage;

        public float CurrentLoadPercentage
        {
            get => _currentLoadPercentage;
            set
            {
                _currentLoadPercentage = value;

                EnableInClassList("not-full", false);
                EnableInClassList("almost-full", false);
                EnableInClassList("full", false);

                if (_currentLoadPercentage < 0.5f)
                {
                    EnableInClassList("not-full", true);
                }
                else if (_currentLoadPercentage < 0.9f)
                {
                    EnableInClassList("almost-full", true);
                }
                else
                {
                    EnableInClassList("full", true);
                }

                _currentLoadPercentageText.text = (_currentLoadPercentage * 100).ToString("F1") + "%";
            }
        }

        // Information
        private VisualElement _informationContainer;
        private VisualElement _iconContainer;
        private Image _icon;
        private TextElement _assigningText;
        private VisualElement _textContainer;
        private TextElement _addressText;
        private TextElement _currentLoadPercentageText;

        // Logs
        private VisualElement _logsContainer;

        public McpListEntry(McpData mcpData, bool isTaskAssigning) : base(nameof(McpListEntry))
        {
            McpData = mcpData;
            _isTaskAssigning = isTaskAssigning;

            ConfigureUss(nameof(McpListEntry));

            AddToClassList("white-button");
            AddToClassList("iconless-button");
            AddToClassList("rounded-button-16px");

            CreateInformation(mcpData);

            if (_isTaskAssigning)
            {
                AddToClassList("task-assigning");
                Clicked += TaskAssigningMcpClickedHandler;
                ChooseMcpsStep.McpListChanged += RefreshAssigningStatus;
                ChooseMcpsStep.OrderSettingChanged += RefreshAssigningStatus;
            }
            else
            {
                Clicked += () => MapManager.Instance.ZoomToMcp(mcpData.Id);
                CreateLogs();
            }

            DataStoreManager.Mcps.FillLevel.DataUpdated += DataUpdatedHandler;
            DataUpdatedHandler(DataStoreManager.Mcps.FillLevel.Data);
        }

        ~McpListEntry()
        {
            DataStoreManager.Mcps.FillLevel.DataUpdated -= DataUpdatedHandler;
        }


        private void CreateInformation(McpData mcpData)
        {
            _informationContainer = new VisualElement { name = "InformationContainer" };
            Add(_informationContainer);

            CreateIcon();
            CreateMcpName(mcpData);
        }

        private void CreateIcon()
        {
            _iconContainer = new VisualElement { name = "IconContainer" };
            _informationContainer.Add(_iconContainer);

            _icon = new Image { name = "Icon" };
            _iconContainer.Add(_icon);

            if (_isTaskAssigning)
            {
                _assigningText = new TextElement { name = "AssigningText" };
                _assigningText.AddToClassList("title-text");
                _assigningText.AddToClassList("white-text");
                _iconContainer.Add(_assigningText);

                _assigningText.style.display = DisplayStyle.None;
            }
        }

        private void CreateMcpName(McpData mcpData)
        {
            _textContainer = new VisualElement { name = "TextContainer" };
            _informationContainer.Add(_textContainer);

            _addressText = new TextElement { name = "AddressText" };
            _addressText.AddToClassList("normal-text");
            _addressText.AddToClassList("black-text");
            _addressText.text = mcpData.Address;
            _textContainer.Add(_addressText);

            _currentLoadPercentageText = new TextElement { name = "CurrentLoadPercentageText" };
            _currentLoadPercentageText.AddToClassList("sub-text");
            _currentLoadPercentageText.AddToClassList("grey-text");
            _textContainer.Add(_currentLoadPercentageText);
        }

        private void CreateLogs()
        {
            _logsContainer = new VisualElement { name = "LogsContainer" };
            Add(_logsContainer);

            for (int i = 0; i < Random.Range(1, 8); i++)
            {
                _logsContainer.Add(new LogEntry(DateTime.Now.AddDays(i).AddHours(Random.Range(0, 10)).AddMinutes(Random.Range(0, 10) * 15)));
            }
        }

        private void DataUpdatedHandler(McpFillLevelBroadcastData data)
        {
            CurrentLoadPercentage = data.FillLevelsById[McpData.Id];
        }

        private void TaskAssigningMcpClickedHandler()
        {
            if (!ChooseMcpsStep.ChosenMcpIds.Contains(McpData.Id)) ChooseMcpsStep.AddMcp(McpData.Id);
            else ChooseMcpsStep.RemoveMcp(McpData.Id);

            RefreshAssigningStatus();

            MapDrawer.Instance.UpdateAssignedMcps();
            // MapManager.Instance.ZoomToMcp(McpData.Id);
        }

        public void RefreshAssigningStatus()
        {
            if (ChooseMcpsStep.ChosenMcpIds.Contains(McpData.Id))
            {
                EnableInClassList("chosen", true);

                if (ChooseMcpsStep.IsOrdered)
                {
                    _assigningText.style.display = DisplayStyle.Flex;
                    _assigningText.text = ChooseMcpsStep.ChosenMcpIds.IndexOf(McpData.Id) + 1 + "";

                    _icon.style.display = DisplayStyle.None;
                }
                else
                {
                    _icon.EnableInClassList("chosen", true);
                }
            }
            else
            {
                EnableInClassList("chosen", false);

                if (ChooseMcpsStep.IsOrdered)
                {
                    _assigningText.style.display = DisplayStyle.None;
                    _icon.style.display = DisplayStyle.Flex;
                }
                else
                {
                    _icon.EnableInClassList("chosen", false);
                }
            }
        }
    }
}