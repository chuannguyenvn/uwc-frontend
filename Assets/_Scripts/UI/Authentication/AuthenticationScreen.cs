using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authentication;
using Commons.Communications.Authentication;
using Commons.Endpoints;
using LocalizationNS;
using Requests;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;

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
        private VisualElement _faceRecognitionLogo;

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
                _titleText.text = Localization.GetSentence(Sentence.AuthenticationView.LOGIN);
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
            _loginButton.text = Localization.GetSentence(Sentence.AuthenticationView.LOGIN);
            _loginElementsContainer.Add(_loginButton);
            _loginButton.RegisterCallback<ClickEvent>(_ => AuthenticationManager.Instance.Login(_usernameTextField.value, _passwordTextField.value));

            _forgotPasswordButton = new Button { name = "ForgotPasswordButton" };
            _forgotPasswordButton.AddToClassList("sub-text");
            _forgotPasswordButton.AddToClassList("grey-text");
            _forgotPasswordButton.text = Localization.GetSentence(Sentence.AuthenticationView.FORGOT_PASSWORD);
            _loginElementsContainer.Add(_forgotPasswordButton);

            if (!Configs.IS_DESKTOP)
            {
                _faceRecognitionLogo = new VisualElement { name = "FaceRecognitionLogo" };
                _faceRecognitionLogo.style.opacity = 0f;
                Add(_faceRecognitionLogo);

                _faceRecognitionLogo.schedule.Execute(async () =>
                {
                    var time = 0f;
                    var sendFaceTimer = 2000f;
                    await Task.Delay(1000);

                    while (true)
                    {
                        _faceRecognitionLogo.style.opacity = Mathf.Sin(time / 200f);
                        time += 20f;
                        sendFaceTimer -= 20f;

                        if (sendFaceTimer <= 0f)
                        {
                            sendFaceTimer = 2000f;
                            DataStoreManager.Instance.StartCoroutine(RequestHelper.SendPostRequest<LoginResponse>(
                                Endpoints.Authentication.LoginWithFace,
                                new LoginWithFaceRequest
                                {
                                    Username = "driver_driver",
                                    Images = new List<byte[]>
                                    {
                                    }
                                }, (success, result) =>
                                {
                                    if (success) AuthenticationManager.Instance.SuccessfulLoginHandler(result);
                                }));
                        }

                        await Task.Delay(20);
                    }
                });
            }
        }

        private void CreateSplash()
        {
            _splashContainer = new VisualElement { name = "SplashContainer" };
            Add(_splashContainer);
        }
    }
}