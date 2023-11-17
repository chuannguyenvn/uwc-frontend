using System;
using System.Threading.Tasks;
using Commons.Communications.Authentication;
using Commons.Endpoints;
using Microsoft.AspNetCore.SignalR.Client;
using Requests;
using Settings;
using UnityEngine;
using Utilities;

namespace Authentication
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public static event Action LoggedIn;
        public static event Action LoggedOut;
        public static event Action<InitializationData> Initialized;

        public string JWT { get; private set; }
        public int UserAccountId { get; private set; }

        public HubConnection HubConnection { get; private set; }

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

        private async void SuccessfulLoginHandler(LoginResponse response)
        {
            JWT = response.Credentials.JwtToken;
            UserAccountId = response.Credentials.AccountId;

            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://" + Endpoints.DOMAIN + "/hub",
                    options => options.AccessTokenProvider = () => Task.FromResult(JWT))
                .Build();

            await HubConnection.StartAsync();

            LoggedIn?.Invoke();

            Debug.Log("Successfully logged in with JWT: " + JWT + " and UserAccountId: " + UserAccountId);

            Initialized?.Invoke(response.InitializationData);
        }

        protected override void OnApplicationQuit()
        {
            HubConnection?.DisposeAsync();
            base.OnApplicationQuit();
        }
    }
}