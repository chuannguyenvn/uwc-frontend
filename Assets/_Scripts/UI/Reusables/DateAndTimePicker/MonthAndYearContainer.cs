using System;
using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.DateAndTimePicker
{
    public class MonthAndYearContainer : AdaptiveElement
    {
        private TextElement _monthAndYearText;

        public MonthAndYearContainer() : base(nameof(MonthAndYearContainer))
        {
            _monthAndYearText = new TextElement { name = "MonthAndYearText" };
            _monthAndYearText.AddToClassList("sub-text");
            _monthAndYearText.AddToClassList("colored-text");
            Add(_monthAndYearText);
            
            Refresh();
        }
        
        public void Refresh()
        {
            _monthAndYearText.text = DatetimePicker.SelectedDateTime.ToString("MMMM");
        }
    }
}