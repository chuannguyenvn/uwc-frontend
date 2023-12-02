using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Authentication;
using Commons.Communications.Authentication;
using Commons.Communications.Map;
using Commons.Communications.Status;
using Commons.Endpoints;
using Commons.Types;
using InfinityCode.OnlineMapsExamples;
using Newtonsoft.Json;
using Requests;
using UI.Views.Mcps.AssignTaskProcedure;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace Maps
{
    public class MapDrawer : PersistentSingleton<MapDrawer>
    {
        [SerializeField] private Texture2D _driverMapIconTexture;
        [SerializeField] private Texture2D _cleanerMapIconTexture;
        [SerializeField] private Texture2D _notFullMcpMapIconTexture;
        [SerializeField] private Texture2D _almostFullMcpMapIconTexture;
        [SerializeField] private Texture2D _fullMcpMapIconTexture;

        private readonly Dictionary<int, OnlineMapsMarker> _driverMarkers = new();
        private readonly Dictionary<int, OnlineMapsMarker> _cleanerMarkers = new();
        private readonly Dictionary<int, OnlineMapsMarker> _mcpMarkers = new();

        [SerializeField] private List<Texture2D> _fullMcpAssigningIndexTextures;
        [SerializeField] private Texture2D _fullMcpAssigningOverflowIndexTexture;
        [SerializeField] private Texture2D _fullMcpAssigningChosenTexture;
        [SerializeField] private Texture2D _fullMcpAssigningRemoveTexture;

        [SerializeField] private List<Texture2D> _almostFullMcpAssigningIndexTextures;
        [SerializeField] private Texture2D _almostFullMcpAssigningOverflowIndexTexture;
        [SerializeField] private Texture2D _almostFullMcpAssigningChosenTexture;
        [SerializeField] private Texture2D _almostFullMcpAssigningRemoveTexture;

        [SerializeField] private List<Texture2D> _notFullMcpAssigningIndexTextures;
        [SerializeField] private Texture2D _notFullMcpAssigningOverflowIndexTexture;
        [SerializeField] private Texture2D _notFullMcpAssigningChosenTexture;
        [SerializeField] private Texture2D _notFullMcpAssigningRemoveTexture;

        private OnlineMapsDrawingElement _route;
        private bool _isRouteDirty = false;

        private int _focusedWorkerId = -1;

        private WorkerLocationBroadcastData _data = new()
        {
            DriverLocationByIds = new(),
            CleanerLocationByIds = new()
        };

        private OnlineMapsDrawingPoly _selfLocation;

        private void Start()
        {
            AuthenticationManager.Instance.Initialized += UpdateAllMcps;
            DataStoreManager.Map.WorkerLocation.DataUpdated += (data) =>
            {
                _data = data;
                _isRouteDirty = true;
            };
            DataStoreManager.Map.McpLocation.DataUpdated += UpdateAllMcps;
            LocationManager.Instance.LocationUpdated += (_) => UpdateLocation();
        }

        private void Update()
        {
            UpdateAllWorkers(_data);
            if (_isRouteDirty)
            {
                DrawWorkerRoute();
                _isRouteDirty = false;
            }
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
                var fillLevel = DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[id];
                var status = fillLevel switch
                {
                    < 0.5f => McpFillStatus.NotFull,
                    < 0.9f => McpFillStatus.AlmostFull,
                    _ => McpFillStatus.Full
                };
                DrawMcpMarker(id, coordinate, status);
            }
        }

        private void UpdateAllMcps(InitializationData data)
        {
            foreach (var (id, coordinate) in data.McpLocationBroadcastData.LocationByIds.ToList())
            {
                var fillLevel = data.McpFillLevelBroadcastData.FillLevelsById[id];
                var status = fillLevel switch
                {
                    < 0.5f => McpFillStatus.NotFull,
                    < 0.9f => McpFillStatus.AlmostFull,
                    _ => McpFillStatus.Full
                };
                DrawMcpMarker(id, coordinate, status);
            }
        }

        private void DrawDriverMarker(int driverId, Coordinate coordinate, float orientationInDegrees)
        {
            OnlineMapsMarker marker;
            if (_driverMarkers.TryGetValue(driverId, out var driverMarker))
            {
                marker = driverMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude);
                marker.scale = 0.1f;
                // marker.range = new OnlineMapsRange(17, 24);
                marker.texture = _driverMapIconTexture;
                marker.OnClick += (_) => FocusWorker(driverId);
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);

            _driverMarkers[driverId] = marker;
        }

        private void DrawCleanerMarker(int cleanerId, Coordinate coordinate, float orientationInDegrees)
        {
            OnlineMapsMarker marker;
            if (_cleanerMarkers.TryGetValue(cleanerId, out var cleanerMarker))
            {
                marker = cleanerMarker;
            }
            else
            {
                marker = OnlineMapsMarkerManager.instance.Create(coordinate.Longitude, coordinate.Latitude);
                marker.scale = 0.1f;
                // marker.range = new OnlineMapsRange(17, 24);
                marker.texture = _cleanerMapIconTexture;
                marker.OnClick += (_) => FocusWorker(cleanerId);
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);

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
                // marker.range = new OnlineMapsRange(17, 24);
                marker.texture = status switch
                {
                    McpFillStatus.Full => _fullMcpMapIconTexture,
                    McpFillStatus.AlmostFull => _almostFullMcpMapIconTexture,
                    McpFillStatus.NotFull => _notFullMcpMapIconTexture,
                    _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
                };
            }

            marker.SetPosition(coordinate.Longitude, coordinate.Latitude);

            _mcpMarkers[mcpId] = marker;
        }

        private void ShowWorkerRoute(List<Coordinate> route)
        {
            HideWorkerRoute();

            var coordinates = new List<double>();

            coordinates.Add(_data.DriverLocationByIds[_focusedWorkerId].Longitude);
            coordinates.Add(_data.DriverLocationByIds[_focusedWorkerId].Latitude);

            foreach (var coordinate in route)
            {
                coordinates.Add(coordinate.Longitude);
                coordinates.Add(coordinate.Latitude);
            }

            var line = new OnlineMapsDrawingLine(coordinates, new Color(90f / 255f, 146f / 255f, 255f / 255f, 0.5f), 4f);
            OnlineMapsDrawingElementManager.instance.Add(line);
            _route = line;
        }

        private void HideWorkerRoute()
        {
            if (_route != null)
            {
                OnlineMapsDrawingElementManager.instance.Remove(_route);
            }
        }

        private void FocusWorker(int workerId)
        {
            _focusedWorkerId = workerId;

            StopCoroutine(SendRouteRequest());
            _isRouteDirty = true;
            DrawWorkerRoute();
        }

        private void DrawWorkerRoute()
        {
            if (_focusedWorkerId == -1) return;

            StartCoroutine(SendRouteRequest());
        }

        private IEnumerator SendRouteRequest()
        {
            yield return RequestHelper.SendPostRequest<GetWorkingStatusResponse>(Endpoints.Status.GetWorkingStatus, new GetWorkingStatusRequest
                {
                    WorkerId = _focusedWorkerId,
                },
                (success, result) =>
                {
                    if (success)
                    {
                        if (result.FocusedTask != null)
                        {
                            var route = result.DirectionToFocusedTask.Routes[0].Geometry.Coordinates;
                            var coordinatesRoute = route.Select(coordinate => new Coordinate(coordinate[1], coordinate[0])).ToList();
                            ShowWorkerRoute(coordinatesRoute);
                        }
                    }
                });
        }

        private void UpdateLocation()
        {
            if (_selfLocation != null)
            {
                OnlineMapsDrawingElementManager.instance.Remove(_selfLocation);
            }

            var segments = 32;

            double lng = LocationManager.Instance.LastKnownCoordinate.Longitude;
            double lat = LocationManager.Instance.LastKnownCoordinate.Latitude;

            double nlng, nlat;
            var radiusKM = LocationManager.Instance.AccuracyInMeters / 1000;
            OnlineMapsUtils.GetCoordinateInDistance(lng, lat, radiusKM, 90, out nlng, out nlat);

            double tx1, ty1, tx2, ty2;

            OnlineMaps.instance.projection.CoordinatesToTile(lng, lat, 20, out tx1, out ty1);

            OnlineMaps.instance.projection.CoordinatesToTile(nlng, nlat, 20, out tx2, out ty2);

            double r = tx2 - tx1;

            OnlineMapsVector2d[] points = new OnlineMapsVector2d[segments];

            double step = 360d / segments;

            for (int i = 0; i < segments; i++)
            {
                double px = tx1 + Math.Cos(step * i * OnlineMapsUtils.Deg2Rad) * r;
                double py = ty1 + Math.Sin(step * i * OnlineMapsUtils.Deg2Rad) * r;
                OnlineMaps.instance.projection.TileToCoordinates(px, py, 20, out lng, out lat);
                points[i] = new OnlineMapsVector2d(lng, lat);
            }

            OnlineMapsDrawingPoly poly = new OnlineMapsDrawingPoly(points, new Color(90f / 255f, 146f / 255f, 255f / 255f), 3,
                new Color(90f / 255f, 146f / 255f, 255f / 255f, 0.5f));
            OnlineMaps.instance.drawingElementManager.Add(poly);

            _selfLocation = poly;
        }

        public void UpdateAssignedMcps()
        {
            var mcpIds = ChooseMcpsStep.ChosenMcpIds;

            foreach (var (id, marker) in _mcpMarkers)
            {
                var fillLevel = DataStoreManager.Mcps.FillLevel.Data.FillLevelsById[id];
                var status = fillLevel switch
                {
                    < 0.5f => McpFillStatus.NotFull,
                    < 0.9f => McpFillStatus.AlmostFull,
                    _ => McpFillStatus.Full
                };

                if (mcpIds.Contains(id))
                {
                    if (ChooseMcpsStep.IsOrdered)
                    {
                        var index = ChooseMcpsStep.ChosenMcpIds.FindIndex(i => i == id);

                        if (index < 9)
                        {
                            marker.texture = status switch
                            {
                                McpFillStatus.Full => _fullMcpAssigningIndexTextures[index],
                                McpFillStatus.AlmostFull => _almostFullMcpAssigningIndexTextures[index],
                                McpFillStatus.NotFull => _notFullMcpAssigningIndexTextures[index],
                            };
                        }
                        else
                        {
                            marker.texture = status switch
                            {
                                McpFillStatus.Full => _fullMcpAssigningOverflowIndexTexture,
                                McpFillStatus.AlmostFull => _almostFullMcpAssigningOverflowIndexTexture,
                                McpFillStatus.NotFull => _notFullMcpAssigningOverflowIndexTexture,
                            };
                        }
                    }
                    else
                    {
                        marker.texture = status switch
                        {
                            McpFillStatus.Full => _fullMcpAssigningChosenTexture,
                            McpFillStatus.AlmostFull => _almostFullMcpAssigningChosenTexture,
                            McpFillStatus.NotFull => _notFullMcpAssigningChosenTexture,
                        };
                    }
                }
                else
                {
                    marker.texture = status switch
                    {
                        McpFillStatus.Full => _fullMcpMapIconTexture,
                        McpFillStatus.AlmostFull => _almostFullMcpMapIconTexture,
                        McpFillStatus.NotFull => _notFullMcpMapIconTexture,
                    };
                }
            }
        }
    }
}