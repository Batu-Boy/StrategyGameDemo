using UnityEngine;

public class UnitProducer : MonoBehaviour
{
    public Vector2Int _spawnPoint;

    public void ProduceUnit(UnitType unitType, Team team)
    {
        EntityFactory<Unit>.CreateEntity(unitType, _spawnPoint, team);
    }
    
    public void CalculateSpawnPoint(int height, Vector2Int position)
    {
        int dividingHeight = height - 1;
        float halfHeight = dividingHeight / 2f;
        int upperHeight = Mathf.CeilToInt(halfHeight);
        int bottomHeight = dividingHeight - upperHeight;
        
        _spawnPoint = position + Vector2Int.down * (bottomHeight + 1);
    }
}