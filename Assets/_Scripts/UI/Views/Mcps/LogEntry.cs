using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Mcps
{
    public class LogEntry : AdaptiveElement
    {
        private readonly DateTime _timestamp;
        private readonly int _workerId;
        private readonly string _workerName;

        public TextElement TextElement;

        public LogEntry(DateTime timestamp, int workerId = -1, string workerName = "") : base(nameof(LogEntry))
        {
            _timestamp = timestamp;
            _workerId = workerId;
            _workerName = workerName;

            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Mcps/LogEntry"));
            AddToClassList("log-entry");

            TextElement = new TextElement { name = "TextElement" };
            TextElement.AddToClassList("sub-sub-text");
            TextElement.AddToClassList("black-text");
            Add(TextElement);
            
            var timestampText = timestamp.ToString("dd/MM");
            if (timestamp.Date == DateTime.Today.Date)
            {
                timestampText = "Today";
            }
            
            if (workerId != -1)
            {
                TextElement.text = $"{_workerName}|{timestampText}";
            }
            else
            {
                TextElement.text = $"{timestampText}";
            }
        }
    }
}