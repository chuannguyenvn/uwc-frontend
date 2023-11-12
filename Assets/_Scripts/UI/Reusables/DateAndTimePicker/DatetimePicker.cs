using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DatetimePicker : AdaptiveElement
    {
        private MonthAndYearContainer _monthAndYearContainer;
        private DateContainer _dateContainer;
        private TimeContainer _timeContainer;


        public DatetimePicker() : base(nameof(DatetimePicker))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Reusables/DatetimePicker"));
            AddToClassList("datetime-picker");

            CreateMonthAndYear();
            CreateDate();
            CreateTime();
        }

        private void CreateMonthAndYear()
        {
            _monthAndYearContainer = new MonthAndYearContainer();
            Add(_monthAndYearContainer);
        }

        private void CreateDate()
        {
            _dateContainer = new DateContainer();
            Add(_dateContainer);
        }

        private void CreateTime()
        {
            _timeContainer = new TimeContainer();
            Add(_timeContainer);
        }

        public new class UxmlFactory : UxmlFactory<DatetimePicker, UxmlTraits>
        {
        }
    }
}