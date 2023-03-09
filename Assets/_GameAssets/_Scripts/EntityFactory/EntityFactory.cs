using UnityEngine;
/// <summary>
/// Responsible for Creating an Entity.
/// Every code block that needs to create an entity, only needs to call this class.
/// Initializing entity, Registering and grid placements,
/// <seealso cref="EntityDestroyer"/>>
/// </summary>
public static class EntityFactory<T> where T : Entity
{
    public static T CreateEntity(EntityType entityType, Vector2Int position, Team team)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, position, team);
        
        RegistryManager.RegisterEntity(entity);
        GridManager.PlaceEntity(entity, position);

        return entity;
    }
    
    public static T CreateEntity(EntityType entityType, Vector2Int position)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, position, PlayerDataModel.Data.PlayerTeam);
        
        RegistryManager.RegisterEntity(entity);
        GridManager.PlaceEntity(entity, position);

        return entity;
    }
    
    public static T CreateEntity(EntityType entityType, Vector3Int position)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, new Vector2Int(position.x, position.y), PlayerDataModel.Data.PlayerTeam);
        
        RegistryManager.RegisterEntity(entity);
        GridManager.PlaceEntity(entity, new Vector2Int(position.x, position.y));

        return entity;
    }

    public static T LoadEntity(EntityType entityType, Vector2Int position, int health, Team team)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitSave(entityType, position, health, team);       
        
        RegistryManager.RegisterEntity(entity);
        GridManager.PlaceEntity(entity, position);

        return entity;
    }

    public static T LoadEntity(EntityType entityType, Vector3Int position, int health, Team team)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitSave(entityType, new Vector2Int(position.x, position.y), health, team);
        
        RegistryManager.RegisterEntity(entity);
        GridManager.PlaceEntity(entity, new Vector2Int(position.x, position.y));

        return entity;
    }
}