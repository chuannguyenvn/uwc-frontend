using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class TimeContainer : AdaptiveElement
    {
        private TimeEntry _previousTimeText;
        private TimeEntry _currentTimeText;
        private TimeEntry _nextTimeText;

        private VisualElement _previousDateButton;
        private VisualElement _nextDateButton;

        public TimeContainer() : base(nameof(TimeContainer))
        {
            CreatePreviousTimeButton();
            CreateTimeEntries();
            CreateNextTimeButton();
        }

        private void CreateTimeEntries()
        {
            _previousTimeText = new TimeEntry() { name = "PreviousTimeText" };
            _previousTimeText.SetTime(DateTime.Today.AddMinutes(-15));
            Add(_previousTimeText);

            _currentTimeText = new TimeEntry() { name = "CurrentTimeText" };
            _currentTimeText.SetTime(DateTime.Today);
            Add(_currentTimeText);

            _nextTimeText = new TimeEntry() { name = "NextTimeText" };
            _nextTimeText.SetTime(DateTime.Today.AddMinutes(15));
            Add(_nextTimeText);
        }

        private void CreatePreviousTimeButton()
        {
            _previousDateButton = new VisualElement { name = "PreviousDateButton" };
            _previousDateButton.AddToClassList("picker-button");
            _previousDateButton.AddToClassList("previous-picker-button");
            Add(_previousDateButton);
        }

        private void CreateNextTimeButton()
        {
            _nextDateButton = new VisualElement { name = "NextDateButton" };
            _nextDateButton.AddToClassList("picker-button");
            _nextDateButton.AddToClassList("next-picker-button");
            Add(_nextDateButton);
        }
    }
}