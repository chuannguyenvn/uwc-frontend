using System.Collections.Generic;
using Commons.Types;
using UnityEngine;

namespace Utilities
{
    public static class OnlineMapsExtesions
    {
        public static OnlineMapsDrawingPoly OnlineMapsDrawingCircle(double longCenter, double latCenter, double radius, int segmentCount,
            float borderWidth,
            Color borderColor,
            Color fillColor)
        {
            var points = new List<double>();
            for (int i = 0; i < segmentCount; i++)
            {
                var angle = i * Mathf.PI * 2 / segmentCount;
                var dx = Mathf.Cos(angle) * radius;
                var dy = Mathf.Sin(angle) * radius;
                points.Add(longCenter + dx);
                points.Add(latCenter + dy);
            }

            return new OnlineMapsDrawingPoly(points, borderColor, borderWidth, fillColor);
        }

        public static OnlineMapsDrawingPoly OnlineMapsDrawingCircle(Coordinate coordinate, double radius, int segmentCount, float borderWidth,
            Color borderColor,
            Color fillColor)
        {
            return OnlineMapsDrawingCircle(coordinate.Longitude, coordinate.Latitude, radius, segmentCount, borderWidth, borderColor, fillColor);
        }
    }
}