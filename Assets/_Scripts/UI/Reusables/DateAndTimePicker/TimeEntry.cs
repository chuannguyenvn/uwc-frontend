using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class TimeEntry : AdaptiveElement
    {
        private TextElement _timeText;
        private TextElement _amPmText;

        public TimeEntry(bool isMiddle) : base(nameof(TimeEntry))
        {
            AddToClassList("time-text");

            _timeText = new TextElement { name = "DateText" };
            _timeText.AddToClassList("normal-text");
            _timeText.AddToClassList("grey-text");
            Add(_timeText);

            _amPmText = new TextElement { name = "AmPmText" };
            _amPmText.AddToClassList("sub-sub-text");
            _amPmText.AddToClassList("grey-text");
            Add(_amPmText);

            if (isMiddle)
            {
                _timeText.AddToClassList("selected");
                _amPmText.AddToClassList("selected");
            }
        }

        public void SetTime(DateTime time)
        {
            _timeText.text = time.ToString("hh:mm");
            _amPmText.text = time.ToString("tt");
        }
    }
}