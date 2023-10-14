using System;
using System.Collections;

namespace Requests.DataStores.Base
{
    public abstract class DataStore<T>
    {
        public event Action<T> DataUpdated;

        public T Data { get; set; }

        protected void OnDataUpdated(T data)
        {
            Data = data;
            DataUpdated?.Invoke(data);
        }

        protected abstract IEnumerator CreateRequest();

        public void SendRequest()
        {
            DataStoreManager.Instance.StartCoroutine(CreateRequest());
        }
    }
}