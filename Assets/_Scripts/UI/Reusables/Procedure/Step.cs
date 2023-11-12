using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.Procedure
{
    public abstract class Step : AdaptiveElement
    {
        private readonly Flow _flow;
        private readonly int _stepIndex;
        private readonly string _stepTitle;

        private bool _isActive;
        private readonly TextElement _stepTitleText;
        protected readonly VisualElement StepContainer;
        private readonly VisualElement _stepContainerShadow;

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

            _stepContainerShadow = new VisualElement() { name = "StepContainerShadow" };
            _stepContainerShadow.pickingMode = PickingMode.Ignore;
            StepContainer.Add(_stepContainerShadow);

            _stepTitleText.RegisterCallback<ClickEvent>(evt =>
            {
                if (_isActive) Deactivate();
                else Activate();
            });
            Deactivate();
        }

        protected void AddStep(VisualElement element)
        {
            StepContainer.Add(element);
            _stepContainerShadow.PlaceInFront(element);
        }

        private void Activate()
        {
            foreach (var flowStep in _flow.Steps)
            {
                flowStep.Deactivate();
            }

            StepContainer.style.display = DisplayStyle.Flex;
            AddToClassList("active");
            _isActive = true;
        }

        private void Deactivate()
        {
            StepContainer.style.display = DisplayStyle.None;
            RemoveFromClassList("active");
            _isActive = false;
        }
    }
}