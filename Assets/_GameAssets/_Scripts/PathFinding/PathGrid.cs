using UnityEngine;
    
/// <summary>
/// Classic grid with Neighbor Arrangement for cells. for <see cref="PathFinder"/> performance
/// </summary>
public class PathGrid
{
    public readonly int Width;
    public readonly int Height;
    private readonly PathNode[,] _gridArray;
    
    public PathGrid(int width, int height)
    {
        Width = width;
        Height = height;
        
        _gridArray = new PathNode[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                PathNode cell = new PathNode(this, x, y);
                _gridArray[x, y] = cell;
            }
        }
        
        ArrangeNeighbors();
    }

    //DOWN => LEFT => UP => RIGHT
    private void ArrangeNeighbors()
    {
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
        {
            //TODO:maybe open comment for diagonal path finding
            var tempNeighbors = new PathNode[4];
            tempNeighbors[0] = GetNode(x, y - 1);
            //tempNeighbors[1] = GetNode(x - 1, y - 1);
            tempNeighbors[1] = GetNode(x - 1, y);
            //tempNeighbors[3] = GetNode(x - 1, y + 1);
            tempNeighbors[2] = GetNode(x, y + 1);
            //tempNeighbors[5] = GetNode(x + 1, y + 1);
            tempNeighbors[3] = GetNode(x + 1, y);
            //tempNeighbors[7] = GetNode(x + 1, y - 1);
            
            GetNode(x, y).SetNeighbors(tempNeighbors);
        }
    }
    
    public PathNode GetNode(int x, int y)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
        {
            return null;
        }

        return _gridArray[x, y];
    }
    
    public PathNode GetNode(Vector2Int pos)
    {
        if (pos.x >= Width || pos.y >= Height || pos.x < 0 || pos.y < 0)
        {
            return null;
        }
        return _gridArray[pos.x, pos.y];
    }
}