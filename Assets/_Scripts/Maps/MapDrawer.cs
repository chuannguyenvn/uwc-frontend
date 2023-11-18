using System;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Commons.Communications.Authentication;
using Commons.Communications.Map;
using Commons.Types;
using Requests;
using UnityEngine;
using Utilities;

namespace Maps
{
    public class MapDrawer : MonoBehaviour
    {
        [SerializeField] private Texture2D _driverMapIconTexture;
        [SerializeField] private Texture2D _cleanerMapIconTexture;
        [SerializeField] private Texture2D _notFullMcpMapIconTexture;
        [SerializeField] private Texture2D _almostFullMcpMapIconTexture;
        [SerializeField] private Texture2D _fullMcpMapIconTexture;

        private readonly Dictionary<int, OnlineMapsMarker> _driverMarkers = new();
        private readonly Dictionary<int, OnlineMapsMarker> _cleanerMarkers = new();
        private readonly Dictionary<int, OnlineMapsMarker> _mcpMarkers = new();

        private WorkerLocationBroadcastData _data = new()
        {
            DriverLocationByIds = new(),
            CleanerLocationByIds = new()
        };

        private void Start()
        {
            AuthenticationManager.Initialized += UpdateAllMcps;
            DataStoreManager.Map.WorkerLocation.DataUpdated += (data) => _data = data;
            DataStoreManager.Map.McpLocation.DataUpdated += UpdateAllMcps;
        }

        private void Update()
        {
            UpdateAllWorkers(_data);
            OnlineMaps.instance.Redraw();
        }

        private void UpdateAllWorkers(WorkerLocationBroadcastData data)
        {
            foreach (var (id, coordinate) in data.DriverLocationByIds.ToList())
            {
                DrawDriverMarker(id, coordinate, 0f);
            }

            foreach (var (id, coordinate) in data.CleanerLocationByIds.ToList())
            {
                DrawCleanerMarker(id, coordinate, 0f);
            }
        }

        private void UpdateAllMcps(McpLocationBroadcastData data)
        {
            foreach (var (id, coordinate) in data.LocationByIds.ToList())
            {
                DrawMcpMarker(id, coordinate, Utility.GetRandomEnumValue<McpFillStatus>());
            }
        }

        private void UpdateAllMcps(InitializationData data)
        {
            foreach (var (id, coordinate) in data.McpLocationBroadcastData.LocationByIds.ToList())
            {
                DrawMcpMarker(id, coordinate, Utility.GetRandomEnumValue<McpFillStatus>());
            }
        }

        private void DrawDriverMarker(int driverId, Coordinate coordinate, float orientationInDegrees)
        {
            if (coordinate.IsApproximatelyEqualTo(new Coordinate(10.7670552457392, 106.656326672901))) return;

            OnlineMapsMarker marker;
            if (_driverMarkers.TryGetValue(driverId, out var driverMarker))
            {
                marker = driverMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude);
                marker.scale = 0.1f;
                marker.range = new OnlineMapsRange(17, 24);
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);
            marker.texture = _driverMapIconTexture;

            _driverMarkers[driverId] = marker;
        }

        private void DrawCleanerMarker(int cleanerId, Coordinate coordinate, float orientationInDegrees)
        {
            if (coordinate.IsApproximatelyEqualTo(new Coordinate(10.7670552457392, 106.656326672901))) return;

            OnlineMapsMarker marker;
            if (_cleanerMarkers.TryGetValue(cleanerId, out var cleanerMarker))
            {
                marker = cleanerMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude);
                marker.scale = 0.1f;
                marker.range = new OnlineMapsRange(17, 24);
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);
            marker.texture = _cleanerMapIconTexture;

            _cleanerMarkers[cleanerId] = marker;
        }

        private void DrawMcpMarker(int mcpId, Coordinate coordinate, McpFillStatus status)
        {
            OnlineMapsMarker marker;
            if (_mcpMarkers.TryGetValue(mcpId, out var mcpMarker))
            {
                marker = mcpMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude);
                marker.scale = 0.1f;
                marker.range = new OnlineMapsRange(17, 24);
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);
            marker.texture = status switch
            {
                McpFillStatus.Full => _fullMcpMapIconTexture,
                McpFillStatus.AlmostFull => _almostFullMcpMapIconTexture,
                McpFillStatus.NotFull => _notFullMcpMapIconTexture,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };

            _mcpMarkers[mcpId] = marker;
        }

        private OnlineMapsDrawingElement CreateWorkerRouteMarker(List<Coordinate> route)
        {
            throw new NotImplementedException();
        }
    }
}