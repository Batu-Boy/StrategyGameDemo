using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GridManager : MonoBase
{
    [SerializeField] private int Width;
    [SerializeField] private int Height;
    
    private static Grid _grid;

    public override void Initialize()
    {
        base.Initialize();
        InitGrid(Width,Height);
    }

    private void InitGrid(int width, int height)
    {
        Width = width;
        Height = height;
        _grid = new Grid(Width, Height);
        EventManager.OnGridInitialized?.Invoke(_grid);
    }
    
    public void ClearGrid()
    {
        _grid = null;
        EventManager.OnClear?.Invoke();
    }
    
    public static void DisplaceEntity(Entity entity)
    {
        foreach (var settlementPosition in GetSettlementPositions(entity.Type, entity.CurrentPosition))
        {
            if (!_grid.TryGetCell(settlementPosition, out Cell cell)) continue;
            
            cell.Entity = null;
        }
    }
    
    public static void PlaceEntity(Entity entity, Vector2Int centerPosition)
    {
        foreach (var settlementPosition in GetSettlementPositions(entity.Type, centerPosition))
        {
            if (!_grid.TryGetCell(settlementPosition, out Cell cell)) continue;
            
            cell.Entity = entity;
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
        return positions.All(IsPositionValid);
    }
    
    private static bool IsPositionValid(Vector2Int position)
    {
        return _grid.GetCell(position) != null && _grid.GetCell(position).IsEmpty;
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