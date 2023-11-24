using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class RootController : Singleton<RootController>
    {
        public event Action BackButtonPressed;
        
        public UIDocument RootDocument;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButtonPressed?.Invoke();
            }
        }
    }
}