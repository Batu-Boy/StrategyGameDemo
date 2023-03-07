using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    public static void DestroyEntity<T>(T entity) where T : Entity
    {
        GridManager.DisplaceEntity(entity);
        Destroy(entity.gameObject);
        //EntityPool<T>.Instance.ReturnItem(entity); TODO: config pools
    }
}