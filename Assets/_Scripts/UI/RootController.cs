using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

namespace UI.Base
{
    public class RootController : Singleton<RootController>
    {
        public UIDocument RootDocument;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }
    }
}