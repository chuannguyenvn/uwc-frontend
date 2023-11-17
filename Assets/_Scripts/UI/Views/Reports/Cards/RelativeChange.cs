using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Reports.Cards
{
    public class RelativeChange : AdaptiveElement
    {
        private readonly Mode _mode;
        private VisualElement _arrowIcon;
        private TextElement _changeValue;

        public RelativeChange(Mode mode) : base(nameof(RelativeChange))
        {
            _mode = mode;
            if (mode == Mode.None)
            {
                style.display = DisplayStyle.None;
                return;
            }

            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Reports/Cards/RelativeChange"));

            CreateArrowIcon();
            CreateChangeValue();
        }

        private void CreateArrowIcon()
        {
            _arrowIcon = new VisualElement { name = "ArrowIcon" };
            _arrowIcon.AddToClassList("arrow-icon");
            Add(_arrowIcon);
        }

        private void CreateChangeValue()
        {
            _changeValue = new TextElement { name = "ChangeValue" };
            _changeValue.AddToClassList("change-value");
            Add(_changeValue);
        }

        public void UpdateChange(float percentage)
        {
            _changeValue.text = $"{Math.Abs(percentage)}%";

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