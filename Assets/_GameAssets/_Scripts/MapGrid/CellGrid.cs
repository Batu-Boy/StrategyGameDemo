using UnityEngine;

public class CellGrid
{
    private readonly int _width;
    private readonly int _height;
    private readonly Cell[,] _gridArray;

    public CellGrid(int width, int height)
    {
        _width = width;
        _height = height;

        _gridArray = new Cell[width, height];

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var cell = new Cell(x, y);
            _gridArray[x, y] = cell;
        }
    }
    
    private Cell GetCell(int x, int y)
    {
        if (x >= _width || y >= _height || x < 0 || y < 0) return null;

        return _gridArray[x, y];
    }

    [EditorButton]
    public Cell GetCell(Vector2Int pos)
    {
        if (pos.x >= _width || pos.y >= _height || pos.x < 0 || pos.y < 0) return null;

        return _gridArray[pos.x, pos.y];
    }

    public bool TryGetEntity(int x, int y, out Entity outElement)
    {
        if (x >= _width || y >= _height || x < 0 || y < 0)
        {
            outElement = null;
            return false;
        }

        return outElement = _gridArray[x, y].Entity;
    }

    public bool TryGetEntity(Vector2Int position, out Entity outElement)
    {
        if (position.x >= _width || position.y >= _height || position.x < 0 || position.y < 0)
        {
            outElement = null;
            return false;
        }

        return outElement = _gridArray[position.x, position.y].Entity;
        ;
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
        for (var x = 0; x < _width; x++)
        for (var i = 0; i < _height; i++)
        {
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

        outCell = GetCell(position);
        return true;
    }

    public bool TryGetCell(int x, int y, out Cell outCell)
    {
        if (x >= _width || y >= _height || x < 0 || y < 0)
        {
            Debug.Log($"The {x},{y} is not on Grid");
            outCell = null;
            return false;
        }

        outCell = GetCell(x, y);
        return true;
    }
}