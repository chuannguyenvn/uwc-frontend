using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class TimeContainer : AdaptiveElement
    {
        private readonly DateTimePicker _dateTimePicker;
        
        private TimeEntry _previousTimeText;
        private TimeEntry _currentTimeText;
        private TimeEntry _nextTimeText;

        private VisualElement _previousDateButton;
        private VisualElement _nextDateButton;

        public TimeContainer(DateTimePicker dateTimePicker) : base(nameof(TimeContainer))
        {
            _dateTimePicker = dateTimePicker;
            
            CreatePreviousTimeButton();
            CreateTimeEntries();
            CreateNextTimeButton();
        }

        private void CreateTimeEntries()
        {
            _previousTimeText = new TimeEntry(false) { name = "PreviousTimeText" };
            Add(_previousTimeText);

            _currentTimeText = new TimeEntry(true) { name = "CurrentTimeText" };
            Add(_currentTimeText);

            _nextTimeText = new TimeEntry(false) { name = "NextTimeText" };
            Add(_nextTimeText);
            
            _previousTimeText.RegisterCallback<ClickEvent>(ev => _dateTimePicker.SelectedDateTime = _dateTimePicker.SelectedDateTime.AddMinutes(-15));
            _nextTimeText.RegisterCallback<ClickEvent>(ev => _dateTimePicker.SelectedDateTime = _dateTimePicker.SelectedDateTime.AddMinutes(15));
        }

        private void CreatePreviousTimeButton()
        {
            _previousDateButton = new VisualElement { name = "PreviousDateButton" };
            _previousDateButton.AddToClassList("picker-button");
            _previousDateButton.AddToClassList("previous-picker-button");
            Add(_previousDateButton);

            _previousDateButton.RegisterCallback<ClickEvent>(ev => _dateTimePicker.SelectedDateTime = _dateTimePicker.SelectedDateTime.AddMinutes(-15));
        }

        private void CreateNextTimeButton()
        {
            _nextDateButton = new VisualElement { name = "NextDateButton" };
            _nextDateButton.AddToClassList("picker-button");
            _nextDateButton.AddToClassList("next-picker-button");
            Add(_nextDateButton);

            _nextDateButton.RegisterCallback<ClickEvent>(ev => _dateTimePicker.SelectedDateTime = _dateTimePicker.SelectedDateTime.AddMinutes(15));
        }

        public void Refresh()
        {
            var now = _dateTimePicker.SelectedDateTime;
            var roundedTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            roundedTime = roundedTime.AddMinutes(Mathf.CeilToInt(now.Minute / 15f) * 15);

            _previousTimeText.SetTime(roundedTime.AddMinutes(-15));
            _currentTimeText.SetTime(roundedTime);
            _nextTimeText.SetTime(roundedTime.AddMinutes(15));
        }
    }
}