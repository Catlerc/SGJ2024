using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Shape
{
    public Vector2Int[] points;
    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

    public Shape rotate()
    {
        var newPoints = new List<Vector2Int>();
        foreach (var point in points)
        {
            var newX = point.y;
            var newY = -point.x;
            newPoints.Add(new Vector2Int(newX, newY));
        }

        var bounds = calculateMinMax(newPoints);
        return new Shape
        {
            points = newPoints.ToArray(),
            minX = bounds[0],
            maxX = bounds[1],
            minY = bounds[2],
            maxY = bounds[3],
        };
    }

    public Shape withCustomOrigin(Vector2Int origin)
    {
        var newPoints = new List<Vector2Int>();
        foreach (var point in points)
        {
            var newX = point.x - origin.x;
            var newY = point.y - origin.y;
            newPoints.Add(new Vector2Int(newX, newY));
        }

        var bounds = calculateMinMax(newPoints);
        return new Shape
        {
            points = newPoints.ToArray(),
            minX = bounds[0],
            maxX = bounds[1],
            minY = bounds[2],
            maxY = bounds[3],
        };
    }

    public static int[] calculateMinMax(List<Vector2Int> points) // => minX, maxX, minY, maxY
    {
        int minX = 99999999;
        int maxX = -99999999;
        int minY = 99999999;
        int maxY = -99999999;

        foreach (var point in points)
        {
            minX = Math.Min(minX, point.x);
            maxX = Math.Max(maxX, point.x);
            minY = Math.Min(minY, point.y);
            maxY = Math.Max(maxY, point.y);
        }

        return new[] { minX, maxX, minY, maxY };
    }

    public static Shape fromStrings(string[] strings)
    {
        Vector2Int? center = null;
        var placesRaw = new List<Vector2Int>();
        for (int y = 0; y < strings.Length; y++)
        {
            var line = strings[strings.Length - y - 1];
            for (int x = 0; x < line.Length; x++)
            {
                var ch = line[x];
                var pos = new Vector2Int(x, y);
                if (ch != '0') placesRaw.Add(pos);
                if (ch == '2') center = pos;
            }
        }

        var places = new List<Vector2Int>();

        foreach (var placeRaw in placesRaw)
        {
            places.Add(placeRaw - center.Value);
        }


        var bounds = calculateMinMax(places);

        return new Shape
        {
            points = places.ToArray(),
            minX = bounds[0],
            maxX = bounds[1],
            minY = bounds[2],
            maxY = bounds[3],
        };
    }
}