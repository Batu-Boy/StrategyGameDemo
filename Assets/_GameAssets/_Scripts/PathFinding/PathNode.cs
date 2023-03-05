using System;
using UnityEngine;

public class PathNode
{
    private PathGrid _grid;
    public bool IsEmpty;
    public Vector2Int Position => new Vector2Int(x, y);
    public int x;
    public int y;
    
    public int g;
    public int h;
    public int f;

    public PathNode CameFromNode; 
    
    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    [NonSerialized] private PathNode[] _neighbors = new PathNode[8];
    public PathNode(PathGrid grid,int x, int y)
    {
        _grid = grid;
        this.x = x;
        this.y = y;
        IsEmpty = true;
    }

    public void ResetValues()
    {
        g = int.MaxValue;
        CalculateF();
        CameFromNode = null;
    }

    public void CalculateF()
    {
        f = g + h;
    }
    
    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    public void SetNeighbors(PathNode[] neighbors)
    {
        _neighbors = neighbors;
    }

    public PathNode[] GetNeighbors()
    {
        return _neighbors;
    }
}