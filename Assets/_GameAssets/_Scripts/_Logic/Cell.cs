using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class Cell
{
    private readonly int _x;
    private readonly int _y;

    [SerializeReference] private Entity _element;
    public bool IsEmpty => _element == null;
    
    [NonSerialized]
    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    private Cell[] _neighbors = new Cell[8];
    
    public Cell(int x, int y)
    {
        _x = x;
        _y = y;
        _element = null;
    }

    public Cell(int x, int y, Entity element)
    {
        _x = x;
        _y = y;
        _element = element;
    }

    public Entity GetElement() => _element;
    public void SetElement(Entity element)
    {
       _element = element;
    }
    
    public bool TryGetElementAs<T>(out T outElement) where T : Entity
    {
        var element = GetElement();
        outElement = element as T;
        if (element == null) return false;
        
        return element.GetType() == typeof(T);
    }

    public Vector3Int GetPosition() => new Vector3Int(_x, _y);

    public void ClearCell() => _element = null;
    
    //DOWN = 0, LEFT = 1, UP = 2, RIGHT = 3
    public Cell[] GetNeighbors() => _neighbors;
    public void SetNeighbors(Cell[] neighbors)
    {
        _neighbors = neighbors;
    }
    // Down->Left->Up->Right
    public Cell GetNeighborAt(int directionIndex) => _neighbors[directionIndex];
    public void SetNeighborAt(Cell neighbor, int directionIndex)
    {
        _neighbors[directionIndex] = neighbor;
    }

}
