using System;
using System.Collections;
using UnityEngine;

namespace Requests.DataStores
{
    public abstract class DataWatcher
    {
        protected Request Request;
        protected float RefreshRate = 5f;

        public Action DataRequested;
        public bool IsFocused { get; private set; } = false;
        private Coroutine _routine;

        public void Focus()
        {
            IsFocused = true;
            StartRequesting();
        }

        public void Unfocus()
        {
            IsFocused = false;
            StopRequesting();
        }

        private IEnumerator ConstructRoutine()
        {
            while (true)
            {
                yield return Request.Send();
                yield return new WaitForSeconds(RefreshRate);
            }
        }

        private void StartRequesting()
        {
            _routine = DataWatcherManager.Instance.StartCoroutine(ConstructRoutine());
        }

        private void StopRequesting()
        {
            DataWatcherManager.Instance.StopCoroutine(_routine);
        }
    }
}