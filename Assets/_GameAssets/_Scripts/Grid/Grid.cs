using UnityEngine;
public class Grid
{
    public int Width { get; }
    public int Height { get; }
    
    private readonly Cell[,] _gridArray;

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        
        _gridArray = new Cell[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = new Cell(x, y);
                _gridArray[x, y] = cell;
            }
        }

        ArrangeNeighbors();
    }

    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    private void ArrangeNeighbors()
    {
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
        {
            //Don't need controls actually
            var tempNeighbors = new Cell[8];
            tempNeighbors[0] = GetCell(x, y - 1);
            tempNeighbors[1] = GetCell(x - 1, y - 1);
            tempNeighbors[2] = GetCell(x - 1, y);
            tempNeighbors[3] = GetCell(x - 1, y + 1);
            tempNeighbors[4] = GetCell(x, y + 1);
            tempNeighbors[5] = GetCell(x + 1, y + 1);
            tempNeighbors[6] = GetCell(x + 1, y);
            tempNeighbors[7] = GetCell(x + 1, y - 1);
            
            GetCell(x, y).SetNeighbors(tempNeighbors);
        }
    }

    public Cell GetCell(int x, int y)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
        {
            return null;
        }
        
        return _gridArray[x, y];
    }
    
    private Cell GetCell(Vector3Int pos)
    {
        if (pos.x >= Width || pos.y >= Height || pos.x < 0 || pos.y < 0)
        {
            return null;
        }
        return _gridArray[pos.x, pos.y];
    }
    
    public bool TryGetElementAs<T>(int x, int y, out T outElement) where T : Entity
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
        {
            outElement = null;
            return false;
        }
        
        var success = _gridArray[x, y].TryGetElementAs<T>(out var element);

        outElement = element;
        return success;
    }


    public Cell[,] GetArray()
    {
        return _gridArray;
    }

    public void ClearAllCells()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int i = 0; i < Height; i++)
            {
                
            }
        }
    }

    public bool TryGetCell(Vector3Int position, out Cell outCell)
    {
        if (position.x >= Width || position.y >= Height || position.x < 0 || position.y < 0)
        {
            Debug.Log($"The {position} is not on Grid");
            outCell = null;
            return false;
        }
        else
        {
            outCell = GetCell(position);
            return true;
        }
    }
    
    public bool TryGetCell(int x, int y, out Cell outCell)
    {
        if (x >= Width || y >= Height || x < 0 || y < 0)
        {
            Debug.Log($"The {x},{y} is not on Grid");
            outCell = null;
            return false;
        }
        else
        {
            outCell = GetCell(x,y);
            return true;
        }
    }
}