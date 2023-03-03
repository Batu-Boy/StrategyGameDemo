using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityPoolModel : MonoBase
{
    public static EntityPoolModel Instance;

    [SerializeField] private Transform _parent;
    [SerializeField] private Entity prefab;
    [SerializeField] private int capacity = 100;
    [SerializeField] List<Entity> items;
    
    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }
    
    public T GetItem<T>(Transform parent = null) where T: Entity
    {
        if (items.Count <= 0)
        {
            return Instantiate(prefab as T, parent);
        }

        var item = items.FirstOrDefault(x => x.GetType() == typeof(T));
        if (!item)
        {
            return Instantiate(prefab as T, parent);
        }
        item.SetActiveGameObject(true);
        items.Remove(item);
        item.transform.SetParent(parent != null ? parent : _parent);
        return item as T;
    }

    public void ReturnItem<T>(T item) where T : Entity
    {
        if (items.Count < capacity)
        {
            item.transform.SetParent(transform);
            item.SetActiveGameObject(false);
            items.Add(item);
        }
        else
        {
            Destroy(item.gameObject);
        }
    }
}
