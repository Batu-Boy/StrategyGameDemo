using UnityEngine;

public class EntityFactory : MonoBehaviour
{
    public static T CreateEntity<T>(EntityType entityType, Vector2Int position) where T : Entity
    {
        //TODO: seperate pools
        var entity = EntityPoolModel.Instance.GetItem<T>();
        entity.InitType(entityType, position);
        return entity;
    }
    
    public static T LoadEntity<T>(EntityType entityType, Vector2Int position, int health) where T : Entity
    {
        var entity = EntityPoolModel.Instance.GetItem<T>();
        entity.InitSave(entityType, position, health);
        return entity;
    }
}