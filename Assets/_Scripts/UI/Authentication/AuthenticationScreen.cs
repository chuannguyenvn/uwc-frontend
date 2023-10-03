using Constants;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Authentication
{
    public class AuthenticationScreen : AdaptiveElement
    {
        public VisualElement LoginElementsContainer;
        public VisualElement Image;
        public TextElement TitleText;
        public TextField UsernameTextField;
        public TextField PasswordTextField;
        public Button LoginButton;
        public Button ForgotPasswordButton;
        public VisualElement SplashContainer;

        public AuthenticationScreen() : base(nameof(AuthenticationScreen))
        {
            styleSheets.Add(Resources.Load<StyleSheet>("Stylesheets/Authentication/AuthenticationScreen"));

            LoginElementsContainer = new VisualElement { name = "LoginElementsContainer" };
            Add(LoginElementsContainer);

            SplashContainer = new VisualElement { name = "SplashContainer" };
            Add(SplashContainer);

            if (Configs.IS_DESKTOP)
            {
                Image = new VisualElement { name = "Image" };
                LoginElementsContainer.Add(Image);
            }

            TitleText = new TextElement { name = "TitleText" };
            if (Configs.IS_DESKTOP)
            {
                TitleText.text = "Urban Waste Collection";
            }
            else
            {
                TitleText.text = "Login";
            }
            TitleText.AddToClassList("title-text");
            TitleText.AddToClassList("colored-text");
            LoginElementsContainer.Add(TitleText);

            UsernameTextField = new TextField { name = "UsernameTextField" };
            UsernameTextField.textEdition.placeholder = "Username";
            UsernameTextField.textEdition.hidePlaceholderOnFocus = true;
            UsernameTextField.AddToClassList("normal-text");
            LoginElementsContainer.Add(UsernameTextField);

            PasswordTextField = new TextField { name = "PasswordTextField" };
            PasswordTextField.isPasswordField = true;
            PasswordTextField.textEdition.placeholder = "Password";
            PasswordTextField.textEdition.hidePlaceholderOnFocus = true;
            PasswordTextField.AddToClassList("normal-text");
            LoginElementsContainer.Add(PasswordTextField);

            LoginButton = new Button { name = "LoginButton" };
            LoginButton.text = "Login";
            LoginButton.AddToClassList("title-text");
            LoginButton.AddToClassList("white-text");
            LoginElementsContainer.Add(LoginButton);
            LoginButton.RegisterCallback<ClickEvent>(_ => { GetFirstAncestorOfType<Root>().CloseAuthenticationScreen(); });

            ForgotPasswordButton = new Button { name = "ForgotPasswordButton" };
            ForgotPasswordButton.text = "Forgot password?";
            ForgotPasswordButton.AddToClassList("sub-text");
            ForgotPasswordButton.AddToClassList("grey-text");
            LoginElementsContainer.Add(ForgotPasswordButton);
        }
    }
}