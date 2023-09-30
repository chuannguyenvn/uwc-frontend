using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace UI.Authentication
{
    public class CredentialsContainer : VisualElement
    {
        private readonly Image _logo;
        private readonly TextElement _title;
        private readonly TextField _username;
        private readonly TextField _password;
        private readonly Button _loginButton;
        private readonly Button _forgotPasswordButton;

        public CredentialsContainer()
        {
            name = "CredentialsContainer";
            AddToClassList("credentials-container");
            
            _logo = new Image() {name = "Logo"};
            _logo.AddToClassList("logo");
            _logo.AddToClassList("colored-image");
            Add(_logo);
            
            _title = new TextElement() {name = "Title", text = "Urban Waste Collection"};
            _title.AddToClassList("title");
            _title.AddToClassList("colored-text");
            Add(_title);
            
            _username = new TextField() {name = "Username"};
            _username.AddToClassList("username");
            Add(_username);
            
            _password = new TextField() {name = "Password"};
            _password.AddToClassList("password");
            Add(_password);
            
            _loginButton = new Button() {name = "LoginButton"};
            _loginButton.AddToClassList("button");
            _loginButton.AddToClassList("login");
            Add(_loginButton);
            
            _forgotPasswordButton = new Button() {name = "ForgotPasswordButton"};
            _forgotPasswordButton.AddToClassList("button");
            _forgotPasswordButton.AddToClassList("forgot-password");
            Add(_forgotPasswordButton);
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<CredentialsContainer, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}