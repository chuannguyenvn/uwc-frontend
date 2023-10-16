using System;
using System.Collections.Generic;
using System.Linq;
using Commons.Communications.Authentication;
using Commons.Communications.Map;
using Requests;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Managers
{
    public class MapManager : PersistentSingleton<MapManager>
    {
        public Dictionary<int, OnlineMapsDrawingElement> WorkerMarkers = new Dictionary<int, OnlineMapsDrawingElement>();
        public Dictionary<int, OnlineMapsDrawingElement> McpMarkers = new Dictionary<int, OnlineMapsDrawingElement>();

        private void Start()
        {
            AuthenticationManager.Initialized += DrawMcps;
        }

        private void OnEnable()
        {
            DataStoreManager.Map.WorkerLocation.DataUpdated += DrawWorkers;
            DataStoreManager.Map.McpLocation.DataUpdated += DrawMcps;
        }

        private void OnDisable()
        {
            DataStoreManager.Map.WorkerLocation.DataUpdated -= DrawWorkers;
            DataStoreManager.Map.McpLocation.DataUpdated -= DrawMcps;
        }

        private void DrawWorkers(WorkerLocationBroadcastData data)
        {
            WorkerMarkers.Values.ToList().ForEach(element => OnlineMapsDrawingElementManager.instance.Remove(element));
            WorkerMarkers.Clear();
            
            foreach (var (id, coordinate) in data.LocationByIds.ToList())
            {
                var marker = OnlineMapsExtesions.OnlineMapsDrawingCircle(coordinate, 0.0001, 32, 0.0001f, Color.magenta, Color.red);
                OnlineMapsDrawingElementManager.instance.Add(marker);
                WorkerMarkers.Add(id, marker);
            }
        }

        private void DrawMcps(McpLocationBroadcastData data)
        {
            McpMarkers.Values.ToList().ForEach(element => OnlineMapsDrawingElementManager.instance.Remove(element));
            McpMarkers.Clear();
            
            foreach (var (id, coordinate) in data.LocationByIds.ToList())
            {
                var marker = OnlineMapsExtesions.OnlineMapsDrawingCircle(coordinate, 0.0001, 32, 0.0001f, Color.magenta, Color.green);
                OnlineMapsDrawingElementManager.instance.Add(marker);
                McpMarkers.Add(id, marker);
            }
        }
        
        private void DrawMcps(InitializationData data)
        {
            McpMarkers.Values.ToList().ForEach(element => OnlineMapsDrawingElementManager.instance.Remove(element));
            McpMarkers.Clear();
            
            foreach (var (id, coordinate) in data.McpLocationByIds.ToList())
            {
                var marker = OnlineMapsExtesions.OnlineMapsDrawingCircle(coordinate, 0.0001, 32, 0.0001f, Color.magenta, Color.green);
                OnlineMapsDrawingElementManager.instance.Add(marker);
                McpMarkers.Add(id, marker);
            }
        }
    }
}