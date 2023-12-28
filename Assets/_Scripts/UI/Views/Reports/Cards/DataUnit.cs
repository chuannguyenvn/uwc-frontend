using UI.Base;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class DataUnit : AdaptiveElement
    {
        private readonly string _suffix;
        private readonly string _format;

        private TextElement _title;
        private TextElement _value;
        private RelativeChange _relativeChange;

        public DataUnit(string name, RelativeChange.Mode mode, string suffix = "", string format = "") : base(name)
        {
            _suffix = suffix;
            _format = format;

            ConfigureUss(nameof(DataUnit));

            CreateTitle(name);
            CreateValue();
            CreateRelativeChange(mode);
        }

        private void CreateTitle(string name)
        {
            _title = new TextElement() { name = "McpCollectedTitle" };
            _title.AddToClassList("normal-text");
            _title.AddToClassList("grey-text");
            _title.text = name;
            Add(_title);
        }

        private void CreateValue()
        {
            _value = new TextElement() { name = "McpCollectedValue" };
            _value.AddToClassList("report-value-text");
            _value.AddToClassList("black-text");
            _value.text = "Value" + _suffix;
            Add(_value);
        }

        private void CreateRelativeChange(RelativeChange.Mode mode)
        {
            _relativeChange = new RelativeChange(mode);
            if (mode != RelativeChange.Mode.None) _relativeChange.UpdateChange(-1f);
            Add(_relativeChange);
            
            _relativeChange.style.display = DisplayStyle.None;
        }

        public void UpdateValue(float value, float percentage)
        {
            _value.text = value.ToString(_format) + _suffix;
            _relativeChange.UpdateChange(percentage);
        }

        public void UpdateValue(string value)
        {
            _value.text = value;
        }
    }
}