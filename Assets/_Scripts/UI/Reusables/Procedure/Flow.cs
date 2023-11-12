using System.Collections.Generic;
using System.Linq;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Reusables.Procedure
{
    public abstract class Flow : AdaptiveElement
    {
        public List<Step> Steps = new();
        private TextElement _confirmButton;

        public Flow(string name) : base(name)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Reusables/Flow"));
            AddToClassList("flow");

            CreateSteps();
            CreateConfirmButton();

            CheckFlowCompletion();
        }

        protected void AddStep(Step step)
        {
            Steps.Add(step);
            Add(step);
        }

        protected abstract void CreateSteps();

        protected void CreateConfirmButton()
        {
            _confirmButton = new TextElement() { name = "ConfirmButton" };
            _confirmButton.RegisterCallback<ClickEvent>(evt => SubmitResult());
            _confirmButton.text = "Confirm";
            _confirmButton.AddToClassList("white-button");
            _confirmButton.AddToClassList("confirm-button");
            _confirmButton.AddToClassList("normal-text");
            _confirmButton.AddToClassList("black-text");
            Add(_confirmButton);
        }

        public void CheckFlowCompletion()
        {
            if (_confirmButton == null) return;

            var isCompleted = Steps.All(step => step.IsCompleted);

            if (isCompleted)
            {
                _confirmButton.AddToClassList("active-button");
                _confirmButton.AddToClassList("colored-button");
                _confirmButton.AddToClassList("white-text");
                _confirmButton.RemoveFromClassList("inactive-button");
                _confirmButton.RemoveFromClassList("white-button");
                _confirmButton.RemoveFromClassList("black-text");
            }
            else
            {
                _confirmButton.AddToClassList("inactive-button");
                _confirmButton.AddToClassList("white-button");
                _confirmButton.AddToClassList("black-text");
                _confirmButton.RemoveFromClassList("active-button");
                _confirmButton.RemoveFromClassList("colored-button");
                _confirmButton.RemoveFromClassList("white-text");
            }
        }

        public abstract void SubmitResult();
    }
}