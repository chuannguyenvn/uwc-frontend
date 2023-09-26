using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Authentication
{
    public class SplashContainer : VisualElement
    {
        private readonly Image _splash;
        
        public SplashContainer()
        {
            name = "SplashContainer";
            AddToClassList("splash-container");
            
            _splash = new Image {name = "SplashImage"};
            _splash.AddToClassList("splash-image");
            
            Add(_splash);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<SplashContainer, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}