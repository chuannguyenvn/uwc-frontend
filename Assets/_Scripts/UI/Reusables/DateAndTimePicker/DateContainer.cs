using System;
using System.Collections.Generic;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DateContainer : AdaptiveElement
    {
        private readonly DateTimePicker _dateTimePicker;

        private VisualElement _dateEntriesContainer;
        private List<DateEntry> _dateEntries;
        private VisualElement _previousDateButton;
        private VisualElement _nextDateButton;

        public DateContainer(DateTimePicker dateTimePicker) : base(nameof(DateContainer))
        {
            _dateTimePicker = dateTimePicker;

            CreatePreviousDateButton();
            CreateDateEntries();
            CreateNextDateButton();
        }

        private void CreateDateEntries()
        {
            var daysInWeek = new List<DayOfWeek>
            {
                DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday
            };

            _dateEntriesContainer = new VisualElement { name = "DateEntriesContainer" };
            Add(_dateEntriesContainer);

            _dateEntries = new List<DateEntry>();
            for (var i = 0; i < 7; i++)
            {
                var dateEntry = new DateEntry(_dateTimePicker, daysInWeek[i]);
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

            _previousDateButton.RegisterCallback<ClickEvent>(ev => _dateTimePicker.SelectedDateTime = _dateTimePicker.SelectedDateTime.AddDays(-1));
        }

        private void CreateNextDateButton()
        {
            _nextDateButton = new VisualElement { name = "NextDateButton" };
            _nextDateButton.AddToClassList("picker-button");
            _nextDateButton.AddToClassList("next-picker-button");
            Add(_nextDateButton);

            _nextDateButton.RegisterCallback<ClickEvent>(ev => _dateTimePicker.SelectedDateTime = _dateTimePicker.SelectedDateTime.AddDays(1));
        }

        public void Refresh()
        {
            for (var i = 0; i < 7; i++)
            {
                _dateEntries[i].Refresh();
            }
        }
    }
}