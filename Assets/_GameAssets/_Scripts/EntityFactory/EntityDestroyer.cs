using UnityEngine;


public class EntityDestroyer : MonoBehaviour
{
    public static void Destroy<T>(T entity) where T : Entity
    {
        //TODO: seperate pools
        Debug.Log($"{typeof(T)},{entity.Type}");
        GridManager.DisplaceEntity(entity);
        EntityPool<T>.Instance.ReturnItem(entity);
    }
}