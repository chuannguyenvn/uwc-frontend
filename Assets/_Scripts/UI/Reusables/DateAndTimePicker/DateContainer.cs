using System;
using System.Collections.Generic;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DateContainer : AdaptiveElement
    {
        private VisualElement _dateEntriesContainer;
        private List<DateEntry> _dateEntries;
        private VisualElement _previousDateButton;
        private VisualElement _nextDateButton;

        public DateContainer() : base(nameof(DateContainer))
        {
            CreatePreviousDateButton();
            CreateDateEntries();
            CreateNextDateButton();
        }

        private void CreateDateEntries()
        {
            var daysInWeek = new List<string> { "M", "T", "W", "T", "F", "S", "S" };

            _dateEntriesContainer = new VisualElement { name = "DateEntriesContainer" };
            Add(_dateEntriesContainer);

            _dateEntries = new List<DateEntry>();
            for (var i = 0; i < 7; i++)
            {
                var dateEntry = new DateEntry(daysInWeek[i]);
                dateEntry.SetDate(DateTime.Today.AddDays(i));
                _dateEntries.Add(dateEntry);
                _dateEntriesContainer.Add(dateEntry);
            }
        }

        private void CreatePreviousDateButton()
        {
            _previousDateButton = new VisualElement { name = "PreviousDateButton" };
            _previousDateButton.AddToClassList("picker-button");
            _previousDateButton.AddToClassList("previous-picker-button");
            Add(_previousDateButton);
        }

        private void CreateNextDateButton()
        {
            _nextDateButton = new VisualElement { name = "NextDateButton" };
            _nextDateButton.AddToClassList("picker-button");
            _nextDateButton.AddToClassList("next-picker-button");
            Add(_nextDateButton);
        }
    }
}