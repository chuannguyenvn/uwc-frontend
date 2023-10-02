using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Views.Tasks.Details
{
    public class CurrentLoadPanel : Panel
    {
        public VisualElement NumericalLoadContainer;
        public TextElement NumericalLoadTitle;
        public TextElement NumericalLoadText;

        public VisualElement VisualLoadContainer;

        public CurrentLoadPanel() : base(nameof(CurrentLoadPanel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Views/Tasks/Details/CurrentLoadPanel"));
            AddToClassList("rounded-32px");
            
            CreateNumericalLoad();
            CreateVisualLoad();
        }

        private void CreateNumericalLoad()
        {
            NumericalLoadContainer = new VisualElement { name = "NumericalLoadContainer" };
            Add(NumericalLoadContainer);

            NumericalLoadTitle = new TextElement { name = "NumericalLoadTitle" };
            NumericalLoadTitle.AddToClassList("sub-text");
            NumericalLoadTitle.AddToClassList("grey-text");
            NumericalLoadTitle.text = "Numerical Load";
            NumericalLoadContainer.Add(NumericalLoadTitle);

            NumericalLoadText = new TextElement { name = "NumericalLoadText" };
            NumericalLoadText.AddToClassList("normal-text");
            NumericalLoadText.AddToClassList("black-text");
            NumericalLoadText.text = "0.00";
            NumericalLoadContainer.Add(NumericalLoadText);
        }

        private void CreateVisualLoad()
        {
            VisualLoadContainer = new VisualElement { name = "VisualLoadContainer" };
            Add(VisualLoadContainer);
        }
    }
}