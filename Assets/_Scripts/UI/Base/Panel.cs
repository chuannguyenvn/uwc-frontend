using UnityEngine.UIElements;

namespace UI.Base
{
    public class Panel : AdaptiveElement
    {
        // Background
        private VisualElement _background;

        public Panel(string name = "Panel") : base(name)
        {
            ConfigureUss(nameof(Panel));

            CreateBackground();
        }

        private void CreateBackground()
        {
            _background = new VisualElement { name = "Background" };
            Add(_background);
        }
    }
}