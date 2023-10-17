using System;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Commons.Communications.Authentication;
using Commons.Communications.Map;
using Commons.Types;
using Requests;
using UnityEngine;

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

        private void Start()
        {
            AuthenticationManager.Initialized += UpdateAllMcps;
        }

        private void OnEnable()
        {
            DataStoreManager.Map.WorkerLocation.DataUpdated += UpdateAllWorkers;
            DataStoreManager.Map.McpLocation.DataUpdated += UpdateAllMcps;
        }

        private void OnDisable()
        {
            DataStoreManager.Map.WorkerLocation.DataUpdated -= UpdateAllWorkers;
            DataStoreManager.Map.McpLocation.DataUpdated -= UpdateAllMcps;
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
            
            OnlineMaps.instance.Redraw();
        }

        private void UpdateAllMcps(McpLocationBroadcastData data)
        {
            foreach (var (id, coordinate) in data.LocationByIds.ToList())
            {
                DrawMcpMarker(id, coordinate, McpFillStatus.NotFull);
            }
            
            OnlineMaps.instance.Redraw();
        }

        private void UpdateAllMcps(InitializationData data)
        {
            foreach (var (id, coordinate) in data.McpLocationByIds.ToList())
            {
                DrawMcpMarker(id, coordinate, McpFillStatus.NotFull);
            }
            
            OnlineMaps.instance.Redraw();
        }

        private void DrawDriverMarker(int driverId, Coordinate coordinate, float orientationInDegrees)
        {
            OnlineMapsMarker marker;
            if (_mcpMarkers.TryGetValue(driverId, out var driverMarker))
            {
                marker = driverMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude, _driverMapIconTexture);
                marker.scale = 0.1f;
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);
            marker.rotationDegree = orientationInDegrees;

            _driverMarkers[driverId] = marker;
        }

        private void DrawCleanerMarker(int cleanerId, Coordinate coordinate, float orientationInDegrees)
        {
            OnlineMapsMarker marker;
            if (_mcpMarkers.TryGetValue(cleanerId, out var cleanerMarker))
            {
                marker = cleanerMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude, _cleanerMapIconTexture);
                marker.scale = 0.1f;
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);
            marker.rotationDegree = orientationInDegrees;

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
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);
            marker.texture = status switch
            {
                McpFillStatus.Full => _fullMcpMapIconTexture,
                McpFillStatus.AlmostFull => _almostFullMcpMapIconTexture,
                McpFillStatus.NotFull => _notFullMcpMapIconTexture,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
            marker.Init();

            _mcpMarkers[mcpId] = marker;
        }

        private OnlineMapsDrawingElement CreateWorkerRouteMarker(List<Coordinate> route)
        {
            throw new NotImplementedException();
        }
    }
}