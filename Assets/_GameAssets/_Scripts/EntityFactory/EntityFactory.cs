using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    public static T CreateEntity<T>(EntityType entityType, Vector2Int position) where T : Entity
    {
        //TODO: seperate pools
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, position);
        GridManager.PlaceEntity(entity, position);
        return entity;
    }
    
    public static T CreateEntity<T>(EntityType entityType, Vector3Int position) where T : Entity
    {
        //TODO: seperate pools
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, new Vector2Int(position.x, position.y));
        GridManager.PlaceEntity(entity, new Vector2Int(position.x, position.y));
        return entity;
    }
    
    public static T LoadEntity<T>(EntityType entityType, Vector2Int position, int health) where T : Entity
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitSave(entityType, position, health);
        GridManager.PlaceEntity(entity, position);
        return entity;
    }
    
    public static T LoadEntity<T>(EntityType entityType, Vector3Int position, int health) where T : Entity
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitSave(entityType, new Vector2Int(position.x, position.y), health);
        GridManager.PlaceEntity(entity, new Vector2Int(position.x, position.y));
        return entity;
    }
}