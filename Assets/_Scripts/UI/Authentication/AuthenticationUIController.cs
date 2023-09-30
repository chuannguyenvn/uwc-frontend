using Constants;
using Managers;
using UI.Main;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Authentication
{
    public class AuthenticationUIController : Singleton<AuthenticationUIController>
    {
        [SerializeField] MainUIController _mainUiController;

        [SerializeField] private UIDocument _uiDocument;
        private AuthenticationView _authenticationView;
        private TextField _usernameField;
        private TextField _passwordField;

        private void Start()
        {
            _authenticationView = _uiDocument.rootVisualElement.Q<AuthenticationView>();
            _usernameField = _authenticationView.Q<CredentialsContainer>().Q<TextField>("Username");
            _passwordField = _authenticationView.Q<CredentialsContainer>().Q<TextField>("Password");
            _uiDocument.rootVisualElement.Q<Button>("LoginButton").clickable.clicked += LoginButtonClickedHandler;
            _uiDocument.rootVisualElement.Q<Button>("ForgotPasswordButton").clickable.clicked += ForgotPasswordButtonClickedHandler;

            if (Configs.IS_DEBUGGING) SuccessfulLoginHandler();
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