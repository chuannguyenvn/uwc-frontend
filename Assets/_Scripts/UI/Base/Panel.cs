using UnityEngine.UIElements;

namespace UI.Base
{
    public class Panel : AdaptiveElement
    {
        private readonly VisualElement _background;

        public Panel() : base(nameof(Panel))
        {
            AddToClassList("panel");

            _background = new VisualElement { name = "Background" };
            Add(_background);
        }
    }
}