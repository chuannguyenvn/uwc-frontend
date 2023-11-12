using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.Procedure
{
    public abstract class Step : AdaptiveElement
    {
        private readonly Flow _flow;
        private readonly int _stepIndex;
        private readonly string _stepTitle;

        private readonly TextElement _stepTitleText;
        protected readonly VisualElement StepContainer;

        public Step(Flow flow, int stepIndex, string stepTitle) : base(stepTitle)
        {
            AddToClassList("step");

            _flow = flow;
            _stepIndex = stepIndex;
            _stepTitle = stepTitle;

            _stepTitleText = new TextElement
            {
                name = "StepTitleText",
                text = stepIndex + ". " + stepTitle
            };
            Add(_stepTitleText);

            StepContainer = new VisualElement() { name = "StepContainer" };
            Add(StepContainer);

            RegisterCallback<ClickEvent>(evt => Activate());
        }

        private void Activate()
        {
            foreach (var flowStep in _flow.Steps)
            {
                flowStep.Deactivate();
            }

            StepContainer.style.display = DisplayStyle.Flex;
            AddToClassList("active");
        }

        private void Deactivate()
        {
            StepContainer.style.display = DisplayStyle.None;
            RemoveFromClassList("active");
        }
    }
}