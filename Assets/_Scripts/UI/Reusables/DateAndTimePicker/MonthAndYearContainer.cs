using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class MonthAndYearContainer : AdaptiveElement
    {
        private readonly DateTimePicker _dateTimePicker;

        private TextElement _monthAndYearText;

        public MonthAndYearContainer(DateTimePicker dateTimePicker) : base(nameof(MonthAndYearContainer))
        {
            _dateTimePicker = dateTimePicker;

            CreateMonthAndYear();
        }

        private void CreateMonthAndYear()
        {
            _monthAndYearText = new TextElement { name = "MonthAndYearText" };
            _monthAndYearText.AddToClassList("sub-text");
            _monthAndYearText.AddToClassList("colored-text");
            Add(_monthAndYearText);
        }

        public void Refresh()
        {
            _monthAndYearText.text = _dateTimePicker.SelectedDateTime.ToString("MMMM");
        }
    }
}