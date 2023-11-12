using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DateEntry : AdaptiveElement
    {
        private readonly DayOfWeek _dayInWeek;
        private TextElement _dayInWeekText;
        private TextElement _dateText;

        public DateEntry(DayOfWeek dayInWeek) : base(nameof(DateEntry))
        {
            _dayInWeek = dayInWeek;
            _dayInWeekText = new TextElement { name = "DayInWeekText" };
            _dayInWeekText.AddToClassList("sub-sub-text");
            _dayInWeekText.AddToClassList("grey-text");
            _dayInWeekText.text = dayInWeek.ToString()[0].ToString();
            Add(_dayInWeekText);

            _dateText = new TextElement { name = "DateText" };
            _dateText.AddToClassList("normal-text");
            _dateText.AddToClassList("grey-text");
            Add(_dateText);

            RegisterCallback<ClickEvent>(ev =>
            {
                var now = DatetimePicker.SelectedDateTime;
                var thisIndex = ((int)_dayInWeek + 6) % 7;
                var nowIndex = ((int)now.DayOfWeek + 6) % 7;
                var diff = thisIndex - nowIndex;
                var date = now.AddDays(diff);
                DatetimePicker.SelectedDateTime = date;
            });
        }

        public void Refresh()
        {
            var now = DatetimePicker.SelectedDateTime;
            var thisIndex = ((int)_dayInWeek + 6) % 7;
            var nowIndex = ((int)now.DayOfWeek + 6) % 7;
            var diff = thisIndex - nowIndex;
            var date = now.AddDays(diff);
            _dateText.text = date.ToString("dd").TrimStart('0');

            if (date.Day == now.Day)
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