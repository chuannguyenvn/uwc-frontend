using System;
using System.Collections;
using Commons.Communications.Authentication;
using Constants;
using Requests;
using UnityEngine;

namespace Managers
{
    public class AuthenticationManager : PersistentSingleton<AuthenticationManager>
    {
        public string JWT { get; private set; }

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
    }
}