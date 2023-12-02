﻿using System;
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
        private readonly McpData _mcpData;
        private readonly bool _isTaskAssigning;

        private float _currentLoadPercentage;

        public float CurrentLoadPercentage
        {
            get => _currentLoadPercentage;
            set
            {
                _currentLoadPercentage = value;

                _iconContainer.RemoveFromClassList("not-full");
                _iconContainer.RemoveFromClassList("almost-full");
                _iconContainer.RemoveFromClassList("full");

                if (_currentLoadPercentage < 90)
                {
                    _iconContainer.AddToClassList("not-full");
                }
                else if (_currentLoadPercentage < 100)
                {
                    _iconContainer.AddToClassList("almost-full");
                }
                else
                {
                    _iconContainer.AddToClassList("full");
                }

                _currentLoadPercentageText.text = _currentLoadPercentage.ToString("F2") + "%";
            }
        }

        // Information
        private VisualElement _informationContainer;
        private VisualElement _iconContainer;
        private Image _icon;
        private VisualElement _textContainer;
        private TextElement _addressText;
        private TextElement _currentLoadPercentageText;

        // Logs
        private VisualElement _logsContainer;

        public McpListEntry(McpData mcpData, bool isTaskAssigning) : base(nameof(McpListEntry))
        {
            _mcpData = mcpData;
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
            CurrentLoadPercentage = data.FillLevelsById[_mcpData.Id];
        }

        private void TaskAssigningMcpClickedHandler()
        {
            if (!ChooseMcpsStep.ChosenMcpIds.Contains(_mcpData.Id))
            {
                ChooseMcpsStep.ChosenMcpIds.Add(_mcpData.Id);
                EnableInClassList("chosen", true);
            }
            else
            {
                ChooseMcpsStep.ChosenMcpIds.Remove(_mcpData.Id);
                EnableInClassList("chosen", false);
            }

            MapDrawer.Instance.UpdateAssignedMcps();
            MapManager.Instance.ZoomToMcp(_mcpData.Id);
        }
    }
}