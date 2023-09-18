using System;
using System.Collections;
using Commons.Communications.Authentication;
using Constants;
using UnityEngine;

namespace Managers
{
    public class AuthenticationManager : Singleton<AuthenticationManager>
    {
        public string Username;
        public string Password;
        public string BearerKey { get; private set; }

        private IEnumerator Start()
        {
            yield return Login_CO();
        }

        public IEnumerator Login_CO()
        {
            yield return HttpsClient.SendRequest<LoginResponse>(
                endpoint: Endpoints.Account.LOGIN,
                requestRequestType: HttpsClient.RequestType.POST,
                callback: (success, result) => Debug.Log(result.JwtToken),
                bearerKey: null,
                objectToSend: new LoginRequest()
                {
                    Username = Username,
                    Password = Password
                });
        }
    }
}