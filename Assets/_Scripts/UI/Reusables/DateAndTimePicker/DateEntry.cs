﻿using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DateEntry : AdaptiveElement
    {
        private TextElement _dayInWeekText;
        private TextElement _dateText;

        public DateEntry(string dayInWeek) : base(nameof(DateEntry))
        {
            _dayInWeekText = new TextElement { name = "DayInWeekText" };
            _dayInWeekText.AddToClassList("sub-sub-text");
            _dayInWeekText.AddToClassList("grey-text");
            _dayInWeekText.text = dayInWeek;
            Add(_dayInWeekText);

            _dateText = new TextElement { name = "DateText" };
            _dateText.AddToClassList("normal-text");
            _dateText.AddToClassList("grey-text");
            Add(_dateText);
        }

        public void SetDate(DateTime date)
        {
            _dateText.text = date.ToString("dd");
        }
        
        public void SetSelected(bool selected)
        {
            if (selected)
            {
                _dayInWeekText.AddToClassList("selected");
                _dateText.AddToClassList("selected");
            }
            else
            {
                _dayInWeekText.RemoveFromClassList("selected");
                _dateText.RemoveFromClassList("selected");
            }
        }
    }
}