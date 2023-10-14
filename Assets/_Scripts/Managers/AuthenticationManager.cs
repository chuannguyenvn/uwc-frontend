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
        public event Action LoggedIn;
        public string JWT { get; private set; }
        public HubConnection HubConnection { get; private set; }

        public void Login(string username, string password, Action<bool> callback)
        {
            StartCoroutine(HttpsClient.SendRequest<LoginResponse>(
                Endpoints.Authentication.LOGIN,
                RequestType.POST,
                (success, response) =>
                {
                    if (success)
                    {
                        JWT = response.JwtToken;
                        EstablishHubConnections();
                        LoggedIn?.Invoke();
                        
                        Debug.Log("Successfully logged in with JWT: " + JWT);
                    }
                    else
                    {
                        Debug.LogError("Failed to login.");
                    }

                    callback(success);
                },
                "",
                new LoginRequest
                {
                    Username = username,
                    Password = password,
                    IsFromDesktop = Configs.IS_DESKTOP
                }));
        }

        private async void EstablishHubConnections()
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://" + Endpoints.DOMAIN + "/messaging",
                    options => options.AccessTokenProvider = () => Task.FromResult(JWT))
                .Build();

            await HubConnection.StartAsync();
        }
    }
}