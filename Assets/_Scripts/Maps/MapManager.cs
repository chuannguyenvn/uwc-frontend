using UI.Base;
using Utilities;

namespace Maps
{
    public class MapManager : PersistentSingleton<MapManager>
    {
        private void Update()
        {
            OnlineMaps.instance.control.allowZoom =
                OnlineMaps.instance.control.allowUserControl = !Root.IsMouseOverElement && !Root.IsMouseDownElement;
        }
    }
}