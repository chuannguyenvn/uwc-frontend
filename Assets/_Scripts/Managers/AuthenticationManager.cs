using System;
using System.Collections;
using Commons.Communications.Authentication;
using Constants;
using UnityEngine;

namespace Managers
{
    public class AuthenticationManager : Singleton<AuthenticationManager>
    {
        public string jwtToken { get; private set; }

        public void Login(string username, string password, Action callback)
        {
            StartCoroutine(Login_CO(username, password, callback));
        }

        private IEnumerator Login_CO(string username, string password, Action callback)
        {
            yield return HttpsClient.SendRequest<LoginResponse>(
                endpoint: Endpoints.Account.LOGIN,
                requestRequestType: HttpsClient.RequestType.POST,
                callback: (success, result) =>
                {
                    if (success)
                    {
                        jwtToken = result.JwtToken;
                        callback?.Invoke();
                    }
                    else
                    {
                        Debug.LogError("Can't login.");
                    }
                },
                bearerKey: null,
                objectToSend: new LoginRequest()
                {
                    Username = username,
                    Password = password
                });
        }
    }
}