using UnityEngine;

public class EntityFactory<T> where T : Entity
{
    public static T CreateEntity(EntityType entityType, Vector2Int position)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, position, PlayerDataModel.Data.PlayerTeam);

        GridManager.PlaceEntity(entity, position);

        return entity;
    }

    public static T CreateEntity(EntityType entityType, Vector3Int position)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitType(entityType, new Vector2Int(position.x, position.y), PlayerDataModel.Data.PlayerTeam);

        GridManager.PlaceEntity(entity, new Vector2Int(position.x, position.y));

        return entity;
    }

    public static T LoadEntity(EntityType entityType, Vector2Int position, int health, Team team)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitSave(entityType, position, health, team);

        GridManager.PlaceEntity(entity, position);

        return entity;
    }

    public static T LoadEntity(EntityType entityType, Vector3Int position, int health, Team team)
    {
        var entity = EntityPool<T>.Instance.GetItem();
        entity.InitSave(entityType, new Vector2Int(position.x, position.y), health, team);

        GridManager.PlaceEntity(entity, new Vector2Int(position.x, position.y));

        return entity;
    }
}