using System.Collections.Generic;
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
        }

        protected abstract void CreateSteps();

        protected void CreateConfirmButton()
        {
            _confirmButton = new TextElement() { name = "ConfirmButton" };
            _confirmButton.RegisterCallback<ClickEvent>(evt => SubmitResult());
            _confirmButton.text = "Confirm";
            Add(_confirmButton);
        }

        public abstract void SubmitResult();
    }
}