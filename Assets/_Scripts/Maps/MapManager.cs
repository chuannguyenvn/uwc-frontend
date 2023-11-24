using UI.Base;
using UnityEngine;
using Utilities;

namespace Maps
{
    public class MapManager : PersistentSingleton<MapManager>
    {
        public GameObject MapGameObject;

        private void Update()
        {
            OnlineMaps.instance.control.allowZoom =
                OnlineMaps.instance.control.allowUserControl = !Root.IsMouseOverElement && !Root.IsMouseDownElement;
        }
    }
}