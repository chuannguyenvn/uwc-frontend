using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DateTimePicker : AdaptiveElement
    {
        private MonthAndYearContainer _monthAndYearContainer;
        private DateContainer _dateContainer;
        private TimeContainer _timeContainer;

        public event Action SelectedDateTimeChanged;
        private DateTime _selectedDateTime = DateTime.Now;

        public DateTime SelectedDateTime
        {
            get => _selectedDateTime;
            set
            {
                _selectedDateTime = value;
                SelectedDateTimeChanged?.Invoke();
            }
        }

        public DateTimePicker() : base(nameof(DateTimePicker))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Reusables/DatetimePicker"));
            AddToClassList("datetime-picker");

            CreateMonthAndYear();
            CreateDate();
            CreateTime();

            SelectedDateTimeChanged += Refresh;
        }

        ~DateTimePicker()
        {
            SelectedDateTimeChanged -= Refresh;
        }

        private void CreateMonthAndYear()
        {
            _monthAndYearContainer = new MonthAndYearContainer(this);
            Add(_monthAndYearContainer);
        }

        private void CreateDate()
        {
            _dateContainer = new DateContainer(this);
            Add(_dateContainer);
        }

        private void CreateTime()
        {
            _timeContainer = new TimeContainer(this);
            Add(_timeContainer);
        }

        public void Refresh()
        {
            _monthAndYearContainer.Refresh();
            _dateContainer.Refresh();
            _timeContainer.Refresh();
        }

        public void ResetDateTime()
        {
            SelectedDateTime = DateTime.Now;
        }
    }
}