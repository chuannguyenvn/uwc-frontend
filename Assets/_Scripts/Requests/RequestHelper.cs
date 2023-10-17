using System;
using System.Collections;
using System.Text;
using Authentication;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Requests
{
    public static class RequestHelper
    {
        private enum RequestType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        private static UnityWebRequest ConstructWebRequest(string endpoint, RequestType requestType, string bearerKey,
            object objectToSend = null)
        {
            var requestTypeString = requestType switch
            {
                RequestType.GET => "GET",
                RequestType.POST => "POST",
                RequestType.PUT => "PUT",
                RequestType.DELETE => "DELETE",
                _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
            };

            var webRequest = new UnityWebRequest("https://" + endpoint, requestTypeString);

            if (objectToSend != null)
            {
                var jsonToSend = new UTF8Encoding().GetBytes(JsonConvert.SerializeObject(objectToSend));
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            if (bearerKey != "") webRequest.SetRequestHeader("Authorization", "Bearer " + bearerKey);

            return webRequest;
        }

        private static IEnumerator SendRequest(string endpoint, RequestType requestType, Action<bool> callback, string bearerKey,
            object objectToSend = null)
        {
            var webRequest = ConstructWebRequest(endpoint, requestType, bearerKey, objectToSend);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed request: " + webRequest.url + "\nTitle: " + webRequest.error + "\nContent: " +
                               webRequest.downloadHandler.text);
                callback?.Invoke(false);
                yield break;
            }

            callback?.Invoke(true);
            webRequest.Dispose();
        }

        private static IEnumerator SendRequest<T>(string endpoint, RequestType requestType, Action<bool, T> callback,
            string bearerKey, object objectToSend = null)
        {
            var webRequest = ConstructWebRequest(endpoint, requestType, bearerKey, objectToSend);

            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed request: " + webRequest.url + "\nTitle: " + webRequest.error + "\nContent: " +
                               webRequest.downloadHandler.text);
                callback?.Invoke(false, default);
                yield break;
            }

            var receivedObject = JsonConvert.DeserializeObject<T>(webRequest.downloadHandler.text);
            callback?.Invoke(true, receivedObject);
            webRequest.Dispose();
        }

        public static IEnumerator SendGetRequest(string endpoint, Action<bool> callback = null, bool isAuthorized = true)
        {
            yield return SendRequest(endpoint, RequestType.GET, callback, isAuthorized ? AuthenticationManager.Instance.JWT : "");
        }

        public static IEnumerator SendGetRequest<T>(string endpoint, Action<bool, T> callback = null, bool isAuthorized = true)
        {
            yield return SendRequest<T>(endpoint, RequestType.GET, callback, isAuthorized ? AuthenticationManager.Instance.JWT : "");
        }

        public static IEnumerator SendPostRequest(string endpoint, object objectToSend, Action<bool> callback = null, bool isAuthorized = true)
        {
            yield return SendRequest(endpoint, RequestType.POST, callback, isAuthorized ? AuthenticationManager.Instance.JWT : "", objectToSend);
        }

        public static IEnumerator SendPostRequest<T>(string endpoint, object objectToSend, Action<bool, T> callback = null, bool isAuthorized = true)
        {
            yield return SendRequest<T>(endpoint, RequestType.POST, callback, isAuthorized ? AuthenticationManager.Instance.JWT : "", objectToSend);
        }
    }
}