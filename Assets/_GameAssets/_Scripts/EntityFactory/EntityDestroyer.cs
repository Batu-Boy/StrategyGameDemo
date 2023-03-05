using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    public static void Destroy<T>(T entity) where T : Entity
    {
        GridManager.DisplaceEntity(entity);
        EntityPool<T>.Instance.ReturnItem(entity);
    }
}