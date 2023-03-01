using UnityEngine;

public class EditorFactory : MonoBehaviour
{
    [EditorButton]
    public void CreateEntity(EntityType type, Vector2Int position)
    {
        var unit = EntityFactory.CreateEntity<Entity>(type, position);
    }

    [EditorButton]
    public void CreateBuilding(BuildingType type, Vector2Int position)
    {
        var building = EntityFactory.CreateEntity<Building>(type, position);
    }

    [EditorButton]
    public void CreateUnit(UnitType type, Vector2Int position)
    {
        var unit = EntityFactory.CreateEntity<Unit>(type, position);
    }
}
