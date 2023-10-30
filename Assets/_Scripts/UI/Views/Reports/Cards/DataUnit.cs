using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class DataUnit : AdaptiveElement
    {
        private readonly string _suffix;
        private readonly string _format;

        public TextElement Title;
        public TextElement Value;
        public RelativeChange RelativeChange;

        public DataUnit(string name, RelativeChange.Mode mode, string suffix = "", string format = "") : base(name)
        {
            _suffix = suffix;
            _format = format;

            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/DataUnit"));
            AddToClassList("data-unit");

            Title = new TextElement() { name = "McpCollectedTitle" };
            Title.AddToClassList("sub-text");
            Title.AddToClassList("grey-text");
            Title.text = name;
            Add(Title);

            Value = new TextElement() { name = "McpCollectedValue" };
            Value.AddToClassList("report-value-text");
            Value.AddToClassList("black-text");
            Value.text = "Value" + _suffix;
            Add(Value);

            RelativeChange = new RelativeChange(mode);
            if (mode != RelativeChange.Mode.None) RelativeChange.UpdateChange(1f);
            Add(RelativeChange);
        }

        public void UpdateValue(float value, float percentage)
        {
            Value.text = value.ToString(_format) + _suffix;
            RelativeChange.UpdateChange(percentage);
        }

        public void UpdateValue(string value)
        {
            Value.text = value;
        }
    }
}