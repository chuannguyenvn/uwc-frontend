using System;
using Constants;
using Managers;
using UI.MainController;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Authentication
{
    public class AuthenticationUIController : Singleton<AuthenticationUIController>
    {
        [SerializeField] MainUIController _mainUiController;

        [SerializeField] private UIDocument _uiDocument;
        private AuthenticationScreen _authenticationScreen;
        private TextField _usernameField;
        private TextField _passwordField;

        private void Start()
        {
            _authenticationScreen = _uiDocument.rootVisualElement.Q<AuthenticationScreen>();
            _usernameField = _authenticationScreen.Q<CredentialsContainer>().Q<TextField>("Username");
            _passwordField = _authenticationScreen.Q<CredentialsContainer>().Q<TextField>("Password");
            _uiDocument.rootVisualElement.Q<Button>("LoginButton").clickable.clicked += LoginButtonClickedHandler;
            _uiDocument.rootVisualElement.Q<Button>("ForgotPasswordButton").clickable.clicked += ForgotPasswordButtonClickedHandler;

            if (Debugs.IS_DEBUG) SuccessfulLoginHandler();
        }

        private void LoginButtonClickedHandler()
        {
            AuthenticationManager.Instance.Login(_usernameField.value, _passwordField.value, SuccessfulLoginHandler);
        }

        private void SuccessfulLoginHandler()
        {
            _mainUiController.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void ForgotPasswordButtonClickedHandler()
        {
            Debug.Log("Forgot password button clicked");
        }
    }
}