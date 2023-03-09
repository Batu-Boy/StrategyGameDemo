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