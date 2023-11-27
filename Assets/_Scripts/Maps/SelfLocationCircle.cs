using System;
using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class SelfLocationCircle : MonoBehaviour
    {
        /// <summary>
        /// Reference to the map. If not set, the current instance will be used.
        /// </summary>
        public OnlineMaps map;

        /// <summary>
        /// Animation curve
        /// </summary>
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        /// <summary>
        /// Duration of animation
        /// </summary>
        public float duration = 3;


        /// <summary>
        /// Number of segments
        /// </summary>
        public int segments = 32;

        /// <summary>
        /// This method is called when the script starts
        /// </summary>
        private void Start()
        {
            if (map == null) map = OnlineMaps.instance;
        }

        public void UpdateLocation()
        {
            // Get the coordinates under cursor
            double lng = LocationManager.Instance.LastKnownCoordinate.Longitude;
            double lat = LocationManager.Instance.LastKnownCoordinate.Latitude;
            OnlineMapsVector2d center = new OnlineMapsVector2d(lng, lat);

            // Create a new marker under cursor
            map.markerManager.Create(lng, lat, "Marker " + map.markerManager.Count);

            // Get the coordinate at the desired distance
            double nlng, nlat;
            var radiusKM = LocationManager.Instance.AccuracyInMeters / 1000;
            OnlineMapsUtils.GetCoordinateInDistance(lng, lat, radiusKM, 90, out nlng, out nlat);

            double tx1, ty1, tx2, ty2;

            // Convert the coordinate under cursor to tile position
            map.projection.CoordinatesToTile(lng, lat, 20, out tx1, out ty1);

            // Convert remote coordinate to tile position
            map.projection.CoordinatesToTile(nlng, nlat, 20, out tx2, out ty2);

            // Calculate radius in tiles
            double r = tx2 - tx1;

            // Create a new array for points
            OnlineMapsVector2d[] points = new OnlineMapsVector2d[segments];

            // Calculate a step
            double step = 360d / segments;

            // Calculate each point of circle
            for (int i = 0; i < segments; i++)
            {
                double px = tx1 + Math.Cos(step * i * OnlineMapsUtils.Deg2Rad) * r;
                double py = ty1 + Math.Sin(step * i * OnlineMapsUtils.Deg2Rad) * r;
                map.projection.TileToCoordinates(px, py, 20, out lng, out lat);
                points[i] = new OnlineMapsVector2d(lng, lat);
            }

            // Create a new polygon to draw a circle
            OnlineMapsDrawingPoly poly = new OnlineMapsDrawingPoly(points, Color.red, 3);
            map.drawingElementManager.Add(poly);
        }

        private void Update()
        {
            UpdateLocation();
            map.Redraw();
        }
    }
}