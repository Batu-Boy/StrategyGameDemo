using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public List<Vector2Int> wayPoints;
    public bool IsComplete => wayPoints.Count <= 0;
    
    public Path(List<Vector2Int> wayPoints)
    {
        this.wayPoints = wayPoints;
    }

    public Vector3 GetNextPoint()
    {
        var nextPoint = wayPoints[0];
        wayPoints.RemoveAt(0);
        return nextPoint.ToMapPos();
    }
}