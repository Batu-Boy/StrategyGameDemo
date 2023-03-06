using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBase
{
    public static PathFinder PathFinder;

    private static CellGrid _cellGrid;
    private static PathGrid _pathGrid;
    
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    
    public override void Initialize()
    {
        base.Initialize();
        InitCellGrid();
        InitPathGrid();
        EventManager.OnGridInitialized?.Invoke(_cellGrid);
    }
    
    private void InitPathGrid()
    {
        _pathGrid = new PathGrid(Width, Height);
        PathFinder = new PathFinder(ref _pathGrid);
    }
    
    private void InitCellGrid()
    {
        _cellGrid = new CellGrid(Width, Height);
    }
    
    public void ClearGrid()
    {
        _cellGrid = null;
        EventManager.OnClear?.Invoke();
    }
    
    public static void DisplaceEntity(Entity entity)
    {
        foreach (var settlementPosition in GetSettlementPositions(entity.Type, entity.CurrentPosition))
        {
            if (!_cellGrid.TryGetCell(settlementPosition, out var cell)) continue;

            cell.Clear();
            if (entity.GetType() == typeof(Building))
                _pathGrid.GetNode(settlementPosition).IsEmpty = true;
        }
    }
    
    public static void PlaceEntity(Entity entity, Vector2Int centerPosition)
    {
        foreach (var settlementPosition in GetSettlementPositions(entity.Type, centerPosition))
        {
            if (!_cellGrid.TryGetCell(settlementPosition, out var cell)) continue;

            cell.Entity = entity;
            if (entity.GetType() == typeof(Building))
                _pathGrid.GetNode(settlementPosition).IsEmpty = false;
        }
    }
    
    public static bool IsSettlementValid(EntityType type, Vector2Int centerPosition)
    {
        if (type.StartWidth == 1 && type.StartHeight == 1) return IsPositionEmpty(centerPosition);

        //need good comment
        return IsPositionsEmpty(GetSettlementPositions(type, centerPosition));
    }
    
    private static bool IsPositionsEmpty(IEnumerable<Vector2Int> positions)
    {
        //not going for linq because of performance
        foreach (var position in positions)
            if (!IsPositionEmpty(position))
                return false;

        return true;
    }
    
    public static bool IsPositionEmpty(Vector2Int position)
    {
        return _cellGrid.GetCell(position) != null && _cellGrid.GetCell(position).IsEmpty;
    }

    public static bool TryGetEntityOnCell(Vector2Int position, out Entity outEntity)
    {
        var success = _cellGrid.TryGetEntity(position, out var entity);
        outEntity = entity;
        return success;
    }

    private static IEnumerable<Vector2Int> GetSettlementPositions(EntityType type, Vector2Int centerPosition)
    {
        var settlementPositions = new List<Vector2Int>();

        if (type.StartWidth == 1 && type.StartHeight == 1)
        {
            settlementPositions.Add(centerPosition);
            return settlementPositions;
        }

        var dividingWidth = type.StartWidth - 1;
        var dividingHeight = type.StartHeight - 1;

        var halfWidth = dividingWidth / 2f;
        var halfHeight = dividingHeight / 2f;

        var upperWidth = Mathf.CeilToInt(halfWidth);
        var upperHeight = Mathf.CeilToInt(halfHeight);

        var bottomWidth = dividingWidth - upperWidth;
        var bottomHeight = dividingHeight - upperHeight;

        for (var x = centerPosition.x - bottomWidth; x < centerPosition.x + upperWidth + 1; x++)
        for (var y = centerPosition.y - bottomHeight; y < centerPosition.y + upperHeight + 1; y++)
            settlementPositions.Add(new Vector2Int(x, y));

        return settlementPositions;
    }
}