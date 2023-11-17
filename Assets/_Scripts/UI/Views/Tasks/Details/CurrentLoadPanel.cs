using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class CurrentLoadPanel : Panel
    {
        private VisualElement _numericalLoadContainer;
        private TextElement _numericalLoadTitle;
        private TextElement _numericalLoadText;

        private VisualElement _visualLoadContainer;

        public CurrentLoadPanel() : base(nameof(CurrentLoadPanel))
        {
            ConfigureUss(nameof(CurrentLoadPanel));

            AddToClassList("rounded-32px");

            CreateNumericalLoad();
            CreateVisualLoad();
        }

        private void CreateNumericalLoad()
        {
            _numericalLoadContainer = new VisualElement { name = "NumericalLoadContainer" };
            Add(_numericalLoadContainer);

            _numericalLoadTitle = new TextElement { name = "NumericalLoadTitle" };
            _numericalLoadTitle.AddToClassList("sub-text");
            _numericalLoadTitle.AddToClassList("grey-text");
            _numericalLoadTitle.text = "Numerical Load";
            _numericalLoadContainer.Add(_numericalLoadTitle);

            _numericalLoadText = new TextElement { name = "NumericalLoadText" };
            _numericalLoadText.AddToClassList("normal-text");
            _numericalLoadText.AddToClassList("black-text");
            _numericalLoadText.text = "0.00";
            _numericalLoadContainer.Add(_numericalLoadText);
        }

        private void CreateVisualLoad()
        {
            _visualLoadContainer = new VisualElement { name = "VisualLoadContainer" };
            Add(_visualLoadContainer);
        }
    }
}