using UI.Common;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Authentication
{
    public class AuthenticationView : FullScreenView
    {
        private readonly CredentialsContainer _credentialsContainer;
        private readonly SplashContainer _splashContainer;

        public AuthenticationView() : base("Authentication")
        {
            AddToClassList("authentication");
            
            _credentialsContainer = new CredentialsContainer();
            Add(_credentialsContainer);

            _splashContainer = new SplashContainer();
            Add(_splashContainer);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<AuthenticationView, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}