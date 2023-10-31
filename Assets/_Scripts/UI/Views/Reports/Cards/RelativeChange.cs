using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class RelativeChange : AdaptiveElement
    {
        private readonly Mode _mode;
        public VisualElement ArrowIcon;
        public TextElement ChangeValue;

        public RelativeChange(Mode mode) : base(nameof(RelativeChange))
        {
            _mode = mode;
            if (mode == Mode.None)
            {
                style.display = DisplayStyle.None;
                return;
            }

            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/RelativeChange"));

            ArrowIcon = new VisualElement { name = "ArrowIcon" };
            ArrowIcon.AddToClassList("arrow-icon");
            Add(ArrowIcon);

            ChangeValue = new TextElement { name = "ChangeValue" };
            ChangeValue.AddToClassList("change-value");
            Add(ChangeValue);
        }

        public void UpdateChange(float percentage)
        {
            ChangeValue.text = $"{Math.Abs(percentage)}%";

            if (percentage > 0 && _mode == Mode.HigherIsBetter || percentage < 0 && _mode == Mode.LowerIsBetter)
            {
                AddToClassList("good-change");
                RemoveFromClassList("bad-change");
            }
            else
            {
                AddToClassList("bad-change");
                RemoveFromClassList("good-change");
            }

            if (percentage > 0)
            {
                AddToClassList("increase-change");
                RemoveFromClassList("decrease-change");
            }
            else
            {
                AddToClassList("decrease-change");
                RemoveFromClassList("increase-change");
            }
        }

        public enum Mode
        {
            None,
            HigherIsBetter,
            LowerIsBetter,
        }
    }
}