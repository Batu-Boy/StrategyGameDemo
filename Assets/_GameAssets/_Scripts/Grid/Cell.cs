using System;
using UnityEngine;

public class Cell
{
    [field: SerializeReference] public Entity Element { get; set; }
    
    public Vector2Int Position => new Vector2Int(_x, _y);
    public Cell[] Neighbors => _neighbors;
    public bool IsEmpty => Element == null;
    
    private readonly int _x;
    private readonly int _y;

    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    [NonSerialized] private Cell[] _neighbors = new Cell[8];
    
    public Cell(int x, int y)
    {
        _x = x;
        _y = y;
        Element = null;
    }

    public Cell(int x, int y, Entity element)
    {
        _x = x;
        _y = y;
        Element = element;
    }
    
    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    public void SetNeighbors(Cell[] neighbors)
    {
        _neighbors = neighbors;
    }
    
    public void ClearCell() => Element = null;
    
    public bool TryGetElementAs<T>(out T outElement) where T : Entity
    {
        var element = Element;
        outElement = element as T;
        if (element == null) return false;
        
        return element.GetType() == typeof(T);
    }
    
}
