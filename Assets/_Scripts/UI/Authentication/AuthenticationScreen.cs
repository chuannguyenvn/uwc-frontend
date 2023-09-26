using Constants;
using UnityEngine;
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
            AddToClassList("authentication");

            var commonStylesheet = Resources.Load<StyleSheet>("Stylesheets/Common/AuthenticationScreen");
            styleSheets.Add(commonStylesheet);
            if (Debugs.IS_DESKTOP)
            {
                var desktopStylesheet = Resources.Load<StyleSheet>("Stylesheets/Desktop/AuthenticationScreen");
                styleSheets.Add(desktopStylesheet);
            }
            else
            {
                var mobileStylesheet = Resources.Load<StyleSheet>("Stylesheets/Mobile/AuthenticationScreen");
                styleSheets.Add(mobileStylesheet);
            }
            
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