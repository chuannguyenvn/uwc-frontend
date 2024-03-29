﻿using System;
using LocalizationNS;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class LogEntry : AdaptiveElement
    {
        private readonly DateTime _timestamp;
        private readonly int _workerId;
        private readonly string _workerName;

        private TextElement _logText;

        public LogEntry(DateTime timestamp, int workerId = -1, string workerName = "") : base(nameof(LogEntry))
        {
            _timestamp = timestamp;
            _workerId = workerId;
            _workerName = workerName;

            ConfigureUss(nameof(LogEntry));

            CreateLogText(timestamp, workerId);
        }

        private void CreateLogText(DateTime timestamp, int workerId)
        {
            _logText = new TextElement { name = "TextElement" };
            _logText.AddToClassList("sub-sub-text");
            _logText.AddToClassList("black-text");
            Add(_logText);

            var timestampText = timestamp.ToString("hh:mmtt dd/MM");
            if (timestamp.Date == DateTime.Today.Date)
            {
                timestampText = timestamp.ToString("hh:mmtt") + " " + Localization.GetSentence(Sentence.McpsView.TODAY).ToLower();
            }

            if (workerId != -1)
            {
                _logText.text = $"{_workerName}|{timestampText}";
            }
            else
            {
                _logText.text = $"{timestampText}";
            }
        }
    }
}