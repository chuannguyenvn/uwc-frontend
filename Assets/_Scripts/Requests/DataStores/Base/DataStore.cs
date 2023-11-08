using System;
using System.Collections;

namespace Requests.DataStores.Base
{
    public abstract class DataStore<T>
    {
        public event Action<T> DataUpdated;

        public T Data { get; set; }

        protected virtual void OnDataUpdated(T data)
        {
            Data = data;
            DataUpdated?.Invoke(data);
        }

        protected virtual IEnumerator CreateRequest()
        {
            yield break;
        }

        public void SendRequest()
        {
            DataStoreManager.Instance.StartCoroutine(CreateRequest());
        }

        public void SendRequest(IEnumerator requestRoutine)
        {
            DataStoreManager.Instance.StartCoroutine(requestRoutine);
        }
    }
}