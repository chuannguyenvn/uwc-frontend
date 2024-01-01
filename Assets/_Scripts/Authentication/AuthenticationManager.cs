using System;
using System.Threading.Tasks;
using Commons.Communications.Authentication;
using Commons.Endpoints;
using LocalizationNS;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Requests;
using Settings;
using UI.Base;
using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace Authentication
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public event Action LoggedIn;
        public event Action LoggedOut;
        public event Action<InitializationData> Initialized;

        public string JWT { get; private set; }
        public int UserAccountId { get; private set; }

        public HubConnection HubConnection { get; private set; }

        private void Start()
        {
            RootController.Instance.RootDocument.rootVisualElement.Q<Root>().Create();
        }

        public void Login(string username, string password)
        {
            StartCoroutine(RequestHelper.SendPostRequest<LoginResponse>(
                Endpoints.Authentication.Login,
                new LoginRequest
                {
                    Username = username,
                    Password = password,
                    IsFromDesktop = Configs.IS_DESKTOP
                },
                (success, response) =>
                {
                    if (success)
                    {
                        SuccessfulLoginHandler(response);
                    }
                    else
                    {
                        Debug.LogError("Failed to login.");
                    }
                },
                false));
        }

        public void Logout()
        {
            HubConnection?.DisposeAsync();
        }

        private async void SuccessfulLoginHandler(LoginResponse response)
        {
            JWT = response.Credentials.JwtToken;
            UserAccountId = response.Credentials.AccountId;

            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://" + Endpoints.DOMAIN + "/hub",
                    options => options.AccessTokenProvider = () => Task.FromResult(JWT))
                .Build();

            await HubConnection.StartAsync();

            Initialized?.Invoke(response.InitializationData);

            LoggedIn?.Invoke();
            Debug.Log("Successfully logged in with JWT: " + JWT + " and UserAccountId: " + UserAccountId);

            Localization.LanguageOption = response.InitializationData.Setting.Language;
            PlayerPrefs.SetString("Language", Localization.LanguageOption.ToString());
        }

        protected override async void OnApplicationQuit()
        {
            await HubConnection.StopAsync();
            HubConnection?.DisposeAsync();
            base.OnApplicationQuit();
        }
    }
}