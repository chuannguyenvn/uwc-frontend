using System.Linq;
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

        // Title
        private VisualElement _stepTitleContainer;
        private TextElement _stepTitleText;
        private TextElement _stepSubTitleText;
        private VisualElement _separator;

        // Steps
        private VisualElement _stepContainer;
        private VisualElement _stepContainerShadow;

        protected Step(Flow flow, int stepIndex, bool completeImmediately, string stepTitle, string stepSubTitle = "") : base(stepTitle)
        {
            _flow = flow;
            _stepIndex = stepIndex;
            _completeImmediately = completeImmediately;
            _stepTitle = stepTitle;
            _stepSubTitle = stepSubTitle;

            ConfigureUss(nameof(Step));

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

            _separator = new VisualElement() { name = "Separator" };
            Add(_separator);
        }

        private void CreateSteps()
        {
            _stepContainer = new VisualElement() { name = "StepContainer" };
            Add(_stepContainer);

            _stepContainerShadow = new VisualElement() { name = "StepContainerShadow" };
            _stepContainerShadow.pickingMode = PickingMode.Ignore;
            _stepContainer.Add(_stepContainerShadow);
        }

        protected void AddToContainer(VisualElement element)
        {
            _stepContainer.Add(element);
            _stepContainerShadow.PlaceInFront(element);
        }

        protected virtual void Activate()
        {
            foreach (var flowStep in _flow.Steps.Except(new[] { this }))
            {
                flowStep.Deactivate();
            }

            RegisterCallback<MouseMoveEvent>(TriggerCheckFlowCompletion);
            RegisterCallback<MouseUpEvent>(TriggerCheckFlowCompletion);
            RegisterCallback<KeyDownEvent>(TriggerCheckFlowCompletion);

            _isInteracted = true;
            MarkActive(true);
            MarkComplete(false);

            _flow.CheckFlowCompletion();
        }

        protected virtual void Deactivate()
        {
            UnregisterCallback<MouseMoveEvent>(TriggerCheckFlowCompletion);
            UnregisterCallback<MouseUpEvent>(TriggerCheckFlowCompletion);
            UnregisterCallback<KeyDownEvent>(TriggerCheckFlowCompletion);

            MarkActive(false);
            if (_isInteracted && (CheckStepCompletion() || (_completeImmediately && !IsCompleted))) MarkComplete(true);

            _flow.CheckFlowCompletion();
        }

        protected abstract bool CheckStepCompletion();

        private void TriggerCheckFlowCompletion(EventBase evt)
        {
            _flow.CheckFlowCompletion();
        }

        private void MarkActive(bool isActive)
        {
            _isActive = isActive;

            if (isActive)
            {
                _stepContainer.style.display = DisplayStyle.Flex;
                _stepTitleText.AddToClassList("white-text");
                _stepTitleText.RemoveFromClassList("black-text");
                if (_stepSubTitle != "") _stepSubTitleText.style.display = DisplayStyle.Flex;
                AddToClassList("active");
            }
            else
            {
                _stepContainer.style.display = DisplayStyle.None;
                _stepTitleText.AddToClassList("black-text");
                _stepTitleText.RemoveFromClassList("white-text");
                if (_stepSubTitle != "") _stepSubTitleText.style.display = DisplayStyle.None;
                RemoveFromClassList("active");
            }
        }

        private void MarkComplete(bool isCompleted)
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