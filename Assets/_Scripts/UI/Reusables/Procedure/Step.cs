using UI.Base;
using UnityEngine.UIElements;

namespace UI.Reusables.Procedure
{
    public abstract class Step : AdaptiveElement
    {
        public bool IsCompleted { get; private set; }

        private readonly Flow _flow;
        private readonly int _stepIndex;
        private readonly bool _completeImmediately;
        private readonly string _stepTitle;
        private readonly string _stepSubTitle;

        private bool _isActive;
        private bool _isInteracted;
        private VisualElement _stepTitleContainer;
        private TextElement _stepTitleText;
        private TextElement _stepSubTitleText;

        protected VisualElement StepContainer;
        private VisualElement _stepContainerShadow;

        public Step(Flow flow, int stepIndex, bool completeImmediately, string stepTitle, string stepSubTitle = "") : base(stepTitle)
        {
            AddToClassList("step");

            _flow = flow;
            _stepIndex = stepIndex;
            _completeImmediately = completeImmediately;
            _stepTitle = stepTitle;
            _stepSubTitle = stepSubTitle;

            CreateTitle();
            CreateSteps();
            Deactivate();
        }

        private void CreateTitle()
        {
            _stepTitleContainer = new VisualElement() { name = "StepTitleContainer" };
            Add(_stepTitleContainer);

            _stepTitleText = new TextElement
            {
                name = "StepTitleText",
                text = _stepIndex + ". " + _stepTitle
            };
            _stepTitleText.AddToClassList("normal-text");
            _stepTitleText.AddToClassList("black-text");
            _stepTitleContainer.Add(_stepTitleText);

            _stepSubTitleText = new TextElement
            {
                name = "StepSubTitleText",
                text = _stepSubTitle
            };
            _stepSubTitleText.AddToClassList("sub-text");
            _stepSubTitleText.AddToClassList("white-text");
            _stepTitleContainer.Add(_stepSubTitleText);
            if (_stepSubTitle == "") _stepSubTitleText.style.display = DisplayStyle.None;

            _stepTitleContainer.RegisterCallback<ClickEvent>(evt =>
            {
                if (_isActive) Deactivate();
                else Activate();
            });
        }

        private void CreateSteps()
        {
            StepContainer = new VisualElement() { name = "StepContainer" };
            Add(StepContainer);

            _stepContainerShadow = new VisualElement() { name = "StepContainerShadow" };
            _stepContainerShadow.pickingMode = PickingMode.Ignore;
            StepContainer.Add(_stepContainerShadow);
        }

        protected void AddToContainer(VisualElement element)
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
            _stepTitleText.AddToClassList("white-text");
            _stepTitleText.RemoveFromClassList("black-text");
            if (_stepSubTitle != "") _stepSubTitleText.style.display = DisplayStyle.Flex;
            AddToClassList("active");
            _isActive = true;

            if (IsCompleted && !_completeImmediately) MarkComplete(false);

            _isInteracted = true;
        }

        private void Deactivate()
        {
            StepContainer.style.display = DisplayStyle.None;
            _stepTitleText.AddToClassList("black-text");
            _stepTitleText.RemoveFromClassList("white-text");
            if (_stepSubTitle != "") _stepSubTitleText.style.display = DisplayStyle.None;
            RemoveFromClassList("active");
            _isActive = false;

            if (_isInteracted && (CheckStepCompletion() || (_completeImmediately && !IsCompleted))) MarkComplete(true);
            _flow.CheckFlowCompletion();
        }

        protected abstract bool CheckStepCompletion();

        public void MarkComplete(bool isCompleted)
        {
            IsCompleted = isCompleted;

            if (isCompleted)
            {
                _stepTitleText.text = $"<i><s>{_stepIndex}. {_stepTitle}</s></i>";
                _stepTitleText.RemoveFromClassList("black-text");
                _stepTitleText.AddToClassList("grey-text");
            }
            else
            {
                _stepTitleText.text = $"{_stepIndex}. {_stepTitle}";
                _stepTitleText.RemoveFromClassList("grey-text");
                _stepTitleText.AddToClassList("black-text");
            }
        }
    }
}