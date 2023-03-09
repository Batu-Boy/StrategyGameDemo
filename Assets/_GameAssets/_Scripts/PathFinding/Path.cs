using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Path that units follow
/// </summary>
public class Path
{
    public List<Vector2Int> WayPoints;
    public bool IsComplete => WayPoints.Count <= 0;
    
    public Path(List<Vector2Int> wayPoints)
    {
        WayPoints = wayPoints;
    }

    public void RemovePointFromEnd(int count)
    {
        if (WayPoints.Count < count)
        {
            WayPoints.Clear();
            return;
        }
        for (int i = 0; i < count; i++)
        {
            WayPoints.RemoveAt(WayPoints.Count - 1);
        }
    }

    public Vector3 GetNextPoint()
    {
        var nextPoint = WayPoints[0];
        WayPoints.RemoveAt(0);
        return nextPoint.ToMapPos();
    }
}