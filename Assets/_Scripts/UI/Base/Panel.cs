using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Base
{
    public class Panel : AdaptiveElement
    {
        private readonly VisualElement _background;

        public Panel() : base(nameof(Panel))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Base/Panel"));
            AddToClassList("panel");

            _background = new VisualElement { name = "Background" };
            Add(_background);
        }
    }
}