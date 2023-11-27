using Requests;
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

        public void ZoomToWorker(int workerId)
        {
            var workerLocation = DataStoreManager.Map.WorkerLocation.GetWorkerLocation(workerId);
            OnlineMaps.instance.SetPositionAndZoom(workerLocation.Longitude, workerLocation.Latitude, 17);
        }

        public void ZoomToMcp(int mcpId)
        {
            var mcpLocation = DataStoreManager.Map.McpLocation.Data.LocationByIds[mcpId];
            OnlineMaps.instance.SetPositionAndZoom(mcpLocation.Longitude, mcpLocation.Latitude, 17);
        }
    }
}