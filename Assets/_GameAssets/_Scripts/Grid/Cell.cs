using System;
using UnityEngine;

public class Cell
{
    [field: SerializeReference] public Entity Entity { get; set; }
    
    public Vector2Int Position => new Vector2Int(_x, _y);
    public Cell[] Neighbors => _neighbors;
    public bool IsEmpty => Entity == null;
    
    private readonly int _x;
    private readonly int _y;

    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    [NonSerialized] private Cell[] _neighbors = new Cell[8];
    
    public Cell(int x, int y)
    {
        _x = x;
        _y = y;
        Entity = null;
    }

    public Cell(int x, int y, Entity entity)
    {
        _x = x;
        _y = y;
        Entity = entity;
    }
    
    //DOWN = 0, LEFT = 2, UP = 4, RIGHT = 6
    public void SetNeighbors(Cell[] neighbors)
    {
        _neighbors = neighbors;
    }
    
    public void ClearCell() => Entity = null;
    
    public bool TryGetEntityAs<T>(out T outEntity) where T : Entity
    {
        var entity = Entity;
        outEntity = entity as T;
        if (entity == null) return false;
        
        return entity.GetType() == typeof(T);
    }
    
}
