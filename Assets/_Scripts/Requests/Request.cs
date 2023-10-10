using System;
using System.Collections;
using UnityEngine;

namespace Requests
{
    public abstract class Request
    {
        public readonly string Endpoint;
        public readonly RequestType RequestType;
        public readonly object ObjectToSend;

        protected Request(string endpoint, RequestType requestType, object objectToSend)
        {
            Endpoint = endpoint;
            RequestType = requestType;
            ObjectToSend = objectToSend;
        }

        public abstract IEnumerator Send();
    }

    public class SimpleRequest : Request
    {
        public readonly Action<bool> Callback;

        public SimpleRequest(string endpoint, RequestType requestType, object objectToSend, Action<bool> callback) : base(endpoint,
            requestType, objectToSend)
        {
            Callback = callback;
        }

        public override IEnumerator Send()
        {
            yield return HttpsClient.SendRequest(Endpoint, RequestType, Callback, "", ObjectToSend);
        }
    }

    public class ParameterizedRequest<T> : Request
    {
        public readonly Action<bool, T> Callback;

        public ParameterizedRequest(string endpoint, RequestType requestType, object objectToSend, Action<bool, T> callback) : base(
            endpoint, requestType, objectToSend)
        {
            Callback = callback;
        }

        public override IEnumerator Send()
        {
            yield return HttpsClient.SendRequest<T>(Endpoint, RequestType, Callback, "", ObjectToSend);
        }
    }
}