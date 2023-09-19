using UnityEngine.UIElements;
using UnityEngine.Scripting;

namespace UI.Authentication
{
    public class AuthenticationScreen : VisualElement
    {
        private readonly CredentialsContainer _credentialsContainer;
        private readonly SplashContainer _splashContainer;

        public AuthenticationScreen()
        {
            name = "AuthenticationScreen";

            _credentialsContainer = new CredentialsContainer();
            Add(_credentialsContainer);

            _splashContainer = new SplashContainer();
            Add(_splashContainer);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<AuthenticationScreen, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}