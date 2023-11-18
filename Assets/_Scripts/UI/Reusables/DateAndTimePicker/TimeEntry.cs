using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class TimeEntry : AdaptiveElement
    {
        private readonly bool _isMiddle;
        private TextElement _timeText;
        private TextElement _amPmText;

        public TimeEntry(bool isMiddle) : base(nameof(TimeEntry))
        {
            _isMiddle = isMiddle;

            ConfigureUss(nameof(TimeEntry));

            CreateTimeText();
            CreateAmPmText();
        }

        private void CreateTimeText()
        {
            _timeText = new TextElement { name = "DateText" };
            _timeText.AddToClassList("normal-text");
            _timeText.AddToClassList("grey-text");
            Add(_timeText);

            if (_isMiddle) _timeText.AddToClassList("selected");
        }

        private void CreateAmPmText()
        {
            _amPmText = new TextElement { name = "AmPmText" };
            _amPmText.AddToClassList("sub-sub-text");
            _amPmText.AddToClassList("grey-text");
            Add(_amPmText);

            if (_isMiddle) _amPmText.AddToClassList("selected");
        }

        public void SetTime(DateTime time)
        {
            _timeText.text = time.ToString("hh:mm");
            _amPmText.text = time.ToString("tt");
        }
    }
}