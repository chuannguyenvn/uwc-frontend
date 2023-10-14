using System;
using System.Threading.Tasks;
using Commons.Communications.Authentication;
using Constants;
using Microsoft.AspNetCore.SignalR.Client;
using Requests;
using UnityEngine;

namespace Managers
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public static event Action LoggedIn;
        public static event Action LoggedOut;

        public string JWT { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public void Login(string username, string password)
        {
            StartCoroutine(RequestHelper.SendPostRequest<LoginResponse>(
                Endpoints.Authentication.LOGIN,
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
                        SuccessfulLoginHandler(response.JwtToken);
                    }
                    else
                    {
                        Debug.LogError("Failed to login.");
                    }
                },
                false));
        }

        private async void SuccessfulLoginHandler(string jwt)
        {
            JWT = jwt;

            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://" + Endpoints.DOMAIN + "/hub",
                    options => options.AccessTokenProvider = () => Task.FromResult(JWT))
                .Build();

            await HubConnection.StartAsync();

            LoggedIn?.Invoke();

            Debug.Log("Successfully logged in with JWT: " + JWT);
        }
    }
}