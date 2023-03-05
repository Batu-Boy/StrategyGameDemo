using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBase
{
    [SerializeField] private int Width;
    [SerializeField] private int Height;

    [SerializeField] private Collider boundingBox;

    public static PathFinder PathFinder;
    
    private static CellGrid _cellGrid;
    private static PathGrid _pathGrid;

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
            if (!_cellGrid.TryGetCell(settlementPosition, out Cell cell)) continue;
            
            cell.Entity = null;
            _pathGrid.GetNode(settlementPosition).IsEmpty = true;
        }
    }
    
    public static void PlaceEntity(Entity entity, Vector2Int centerPosition)
    {
        foreach (var settlementPosition in GetSettlementPositions(entity.Type, centerPosition))
        {
            if (!_cellGrid.TryGetCell(settlementPosition, out Cell cell)) continue;
            
            cell.Entity = entity;
            _pathGrid.GetNode(settlementPosition).IsEmpty = false;
        }
    }
    
    public static bool IsSettlementValid(EntityType type, Vector2Int centerPosition)
    {
        if (type.StartWidth == 1 && type.StartHeight == 1)
        {
            return IsPositionValid(centerPosition);
        }
        
        //need good comment
        return IsPositionsValid(GetSettlementPositions(type, centerPosition));
    }
    
    private static bool IsPositionsValid(IEnumerable<Vector2Int> positions)
    {
        //not going for linq because of performance
        foreach (var position in positions)
        {
            if (!IsPositionValid(position))
                return false;
        }

        return true;
    }
    
    public static bool IsPositionValid(Vector2Int position)
    {
        return _cellGrid.GetCell(position) != null && _cellGrid.GetCell(position).IsEmpty;
    }
    
    private static IEnumerable<Vector2Int> GetSettlementPositions(EntityType type, Vector2Int centerPosition)
    {
        List<Vector2Int> settlementPositions = new List<Vector2Int>();
        
        if (type.StartWidth == 1 && type.StartHeight == 1)
        {
            settlementPositions.Add(centerPosition);
            return settlementPositions;
        }
        
        int dividingWidth = type.StartWidth - 1;
        int dividingHeight = type.StartHeight - 1;

        float halfWidth = dividingWidth / 2f;
        float halfHeight = dividingHeight / 2f;
        
        int upperWidth = Mathf.CeilToInt(halfWidth);
        int upperHeight = Mathf.CeilToInt(halfHeight);
        
        int bottomWidth = dividingWidth - upperWidth;
        int bottomHeight = dividingHeight - upperHeight;
        
        for (int x = centerPosition.x - bottomWidth; x < centerPosition.x + upperWidth + 1; x++)
        for (int y = centerPosition.y - bottomHeight; y < centerPosition.y + upperHeight + 1; y++)
        {
            settlementPositions.Add(new Vector2Int(x, y));
        }


        return settlementPositions;
    }
}