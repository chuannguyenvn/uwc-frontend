using Authentication;
using Settings;
using UI.Base;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Authentication
{
    public class AuthenticationScreen : AdaptiveElement
    {
        // Login
        private VisualElement _loginElementsContainer;
        private VisualElement _logo;
        private TextElement _titleText;
        private TextField _usernameTextField;
        private TextField _passwordTextField;
        private Button _loginButton;
        private Button _forgotPasswordButton;

        // Splash
        private VisualElement _splashContainer;

        public AuthenticationScreen() : base(nameof(AuthenticationScreen))
        {
            ConfigureUss(nameof(AuthenticationScreen));

            CreateLogin();
            CreateSplash();
        }

        private void CreateLogin()
        {
            _loginElementsContainer = new VisualElement { name = "LoginElementsContainer" };
            Add(_loginElementsContainer);

            _titleText = new TextElement { name = "TitleText" };
            _titleText.AddToClassList("title-text");
            _titleText.AddToClassList("colored-text");

            if (Configs.IS_DESKTOP)
            {
                _titleText.text = "Urban Waste Collection";

                _logo = new VisualElement { name = "Logo" };
                _loginElementsContainer.Add(_logo);
            }
            else
            {
                _titleText.text = "Login";
            }

            _loginElementsContainer.Add(_titleText);

            _usernameTextField = new TextField { name = "UsernameTextField" };
            _usernameTextField.AddToClassList("normal-text");
            _usernameTextField.AddToClassList("black-text");
            _usernameTextField.textEdition.placeholder = "Username";
            _usernameTextField.textEdition.hidePlaceholderOnFocus = true;
            if (Configs.IS_DEVELOPMENT) _usernameTextField.value = Configs.IS_DESKTOP ? "supervisor_supervisor" : "driver_driver";
            _loginElementsContainer.Add(_usernameTextField);

            _passwordTextField = new TextField { name = "PasswordTextField" };
            _passwordTextField.AddToClassList("normal-text");
            _passwordTextField.AddToClassList("black-text");
            _passwordTextField.isPasswordField = true;
            _passwordTextField.textEdition.placeholder = "Password";
            _passwordTextField.textEdition.hidePlaceholderOnFocus = true;
            if (Configs.IS_DEVELOPMENT) _passwordTextField.value = "password";
            _loginElementsContainer.Add(_passwordTextField);

            _loginButton = new Button { name = "LoginButton" };
            _loginButton.AddToClassList("title-text");
            _loginButton.AddToClassList("white-text");
            _loginButton.text = "Login";
            _loginElementsContainer.Add(_loginButton);
            _loginButton.RegisterCallback<ClickEvent>(_ => AuthenticationManager.Instance.Login(_usernameTextField.value, _passwordTextField.value));

            _forgotPasswordButton = new Button { name = "ForgotPasswordButton" };
            _forgotPasswordButton.AddToClassList("sub-text");
            _forgotPasswordButton.AddToClassList("grey-text");
            _forgotPasswordButton.text = "Forgot password?";
            _loginElementsContainer.Add(_forgotPasswordButton);
        }

        private void CreateSplash()
        {
            _splashContainer = new VisualElement { name = "SplashContainer" };
            Add(_splashContainer);
        }
    }
}