using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class DateEntry : AdaptiveElement
    {
        private readonly DateTimePicker _dateTimePicker;

        private DayOfWeek _dayOfWeek;
        private TextElement _dayOfWeekText;
        private TextElement _dateText;

        public DateEntry(DateTimePicker dateTimePicker, DayOfWeek dayOfWeek) : base(nameof(DateEntry))
        {
            _dateTimePicker = dateTimePicker;

            CreateDayOfWeek(dayOfWeek);
            CreateDate();
            RegisterClickCallback();
        }

        private void CreateDayOfWeek(DayOfWeek dayOfWeek)
        {
            _dayOfWeek = dayOfWeek;
            _dayOfWeekText = new TextElement { name = "DayInWeekText" };
            _dayOfWeekText.AddToClassList("sub-sub-text");
            _dayOfWeekText.AddToClassList("grey-text");
            _dayOfWeekText.text = dayOfWeek.ToString()[0].ToString();
            Add(_dayOfWeekText);
        }

        private void CreateDate()
        {
            _dateText = new TextElement { name = "DateText" };
            _dateText.AddToClassList("normal-text");
            _dateText.AddToClassList("grey-text");
            Add(_dateText);
        }

        private void RegisterClickCallback()
        {
            RegisterCallback<ClickEvent>(ev =>
            {
                var now = _dateTimePicker.SelectedDateTime;
                var thisIndex = ((int)_dayOfWeek + 6) % 7;
                var nowIndex = ((int)now.DayOfWeek + 6) % 7;
                var diff = thisIndex - nowIndex;
                var date = now.AddDays(diff);
                _dateTimePicker.SelectedDateTime = date;
            });
        }

        public void Refresh()
        {
            var now = _dateTimePicker.SelectedDateTime;
            var thisIndex = ((int)_dayOfWeek + 6) % 7;
            var nowIndex = ((int)now.DayOfWeek + 6) % 7;
            var diff = thisIndex - nowIndex;
            var date = now.AddDays(diff);
            _dateText.text = date.ToString("dd").TrimStart('0');

            if (date.Day == now.Day)
            {
                _dayOfWeekText.AddToClassList("selected");
                _dateText.AddToClassList("selected");
            }
            else
            {
                _dayOfWeekText.RemoveFromClassList("selected");
                _dateText.RemoveFromClassList("selected");
            }
        }
    }
}