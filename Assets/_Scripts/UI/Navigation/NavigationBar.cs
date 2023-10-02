using UI.Base;
using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Navigation
{
    public class NavigationBar : AdaptiveElement
    {
        private readonly Panel _background;

        public NavigationBar() : base(nameof(NavigationBar))
        {
            AddToClassList("navigation-bar");

            _background = new Panel();
            _background.AddToClassList("left-bar");
            Add(_background);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<NavigationBar, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}