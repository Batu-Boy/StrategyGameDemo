using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    public static T CreateEntity<T>(EntityType entityType, Vector2Int position) where T : Entity
    {
        var entity = Instantiate(entityType.EntityPrefab);
        entity.InitType(entityType, position);
        return entity as T;
    }
    
    public static T LoadEntity<T>(EntityType entityType, Vector2Int position, int health) where T : Entity
    {
        var entity = Instantiate(entityType.EntityPrefab);
        entity.InitSave(entityType, position, health);
        return entity as T;
    }
}