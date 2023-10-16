using System;
using System.Collections.Generic;
using System.Linq;
using Requests;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Managers
{
    public class MapManager : PersistentSingleton<MapManager>
    {
        private void Start()
        {
            var linePoints = new List<float>
            {
                106.656326f, 10.767055f, 106.68f, 10.78f,
            };

            List<Vector2> polyPoints = new List<Vector2>
            {
                //Geographic coordinates
                new Vector2(0, 0),
                new Vector2(1, 0),
                new Vector2(2, 2),
                new Vector2(0, 1)
            };

            // // Draw line
            // OnlineMapsDrawingLine line = new OnlineMapsDrawingLine(linePoints, Color.green, 5);
            // OnlineMapsDrawingElementManager.instance.Add(line);

            DataStoreManager.Map.Location.DataUpdated += data =>
            {
                OnlineMapsDrawingElementManager.instance.RemoveAll();

                foreach (var (id, coordinate) in data.LocationByIds.ToList())
                {
                    OnlineMapsDrawingElementManager.instance.Add(
                        OnlineMapsExtesions.OnlineMapsDrawingCircle(coordinate.Longitude, coordinate.Latitude, 0.001, 8, 0.0001f, Color.magenta,
                            Color.green));
                }
            };
        }
    }
}