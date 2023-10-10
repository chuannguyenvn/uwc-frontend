using System;
using System.Collections;
using UnityEngine;

namespace Requests.DataStores
{
    public abstract class DataStore
    {
        protected Request Request;
        
        public Action DataRequested;
        public bool IsFocused { get; private set; } = false;
        protected Coroutine Routine;
        
        public void Focus()
        {
            IsFocused = true;
            StartRequesting();
        }

        public void Unfocus()
        {
            IsFocused = false;
        }

        protected void StartRequesting()
        {
            Routine = DataStoreManager.Instance.StartCoroutine(Request.Send());
        }

        protected void StopRequesting()
        {
            DataStoreManager.Instance.StopCoroutine(Routine);
        }
    }
}