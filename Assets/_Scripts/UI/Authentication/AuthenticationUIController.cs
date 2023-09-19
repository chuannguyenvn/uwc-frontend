using System;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Authentication
{
    public class AuthenticationUIController : MonoBehaviour
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
        }

        private void LoginButtonClickedHandler()
        {
            AuthenticationManager.Instance.Login(_usernameField.value, _passwordField.value, () =>
            {
                gameObject.SetActive(false);
                _mainUiController.gameObject.SetActive(true);
            });
        }

        private void ForgotPasswordButtonClickedHandler()
        {
            Debug.Log("Forgot password button clicked");
        }
    }
}