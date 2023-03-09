/// <summary>
/// Responsible for Destroying an Entity.
/// Every code block that needs to destroy an entity, only needs to call this class.
/// Grid displacements, pool returning, and registering from one hand.
/// <seealso cref="EntityFactory{T}"/>>
/// </summary>
public static class EntityDestroyer
{
    public static void DestroyEntity<T>(T entity) where T : Entity
    {
        GridManager.DisplaceEntity(entity);
        EntityPool<T>.Instance.ReturnItem(entity);
        RegistryManager.RemoveEntity(entity);
        ConditionHelper.CheckEnd();
    }
    
    public static void DestroyEntityImmediate<T>(T entity) where T : Entity
    {
        GridManager.DisplaceEntity(entity);
        EntityPool<T>.Instance.ReturnItem(entity);
        RegistryManager.RemoveEntity(entity);
    }
}