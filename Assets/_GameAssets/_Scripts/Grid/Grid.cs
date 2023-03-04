using UnityEngine;
public class Grid
{
    private readonly int _width;
    private readonly int _height;
    private readonly Cell[,] _gridArray;

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
        
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
        for (var x = 0; x < _width; x++)
        for (var y = 0; y < _height; y++)
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

    private Cell GetCell(int x, int y)
    {
        if (x >= _width || y >= _height || x < 0 || y < 0)
        {
            return null;
        }

        return _gridArray[x, y];
    }
    
    [EditorButton]
    public Cell GetCell(Vector2Int pos)
    {
        if (pos.x >= _width || pos.y >= _height || pos.x < 0 || pos.y < 0)
        {
            return null;
        }
        return _gridArray[pos.x, pos.y];
    }
    
    public bool TryGetEntityAs<T>(int x, int y, out T outElement) where T : Entity
    {
        if (x >= _width || y >= _height || x < 0 || y < 0)
        {
            outElement = null;
            return false;
        }
        
        var success = _gridArray[x, y].TryGetEntityAs<T>(out var element);

        outElement = element;
        return success;
    }


    public Cell[,] GetArray()
    {
        return _gridArray;
    }

    public void ClearAllCells()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int i = 0; i < _height; i++)
            {
                
            }
        }
    }

    public bool TryGetCell(Vector2Int position, out Cell outCell)
    {
        if (position.x >= _width || position.y >= _height || position.x < 0 || position.y < 0)
        {
            //Debug.Log($"The {position} is not on Grid");
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
        if (x >= _width || y >= _height || x < 0 || y < 0)
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