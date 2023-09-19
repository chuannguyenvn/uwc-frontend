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
            
            _splash = new Image {name = "Splash"};
            
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