using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityPool<T> : MonoBase where T : Entity
{
    public static EntityPool<T> Instance;
    
    [SerializeField] private Transform _parent;
    [SerializeField] private T prefab;
    [SerializeField] private int capacity = 100;
    [SerializeField] List<T> items;
    
    public override void Initialize()
    {
        base.Initialize();
        Instance = this;
    }
    
    public T GetItem(Transform parent = null)
    {
        Transform returnParent = parent != null ? parent : _parent;
        if (items.Count <= 0)
        {
            return Instantiate(prefab, returnParent);
        }
        
        var item = items.FirstOrDefault();
        
        if(!item)
            return Instantiate(prefab, returnParent);
        
        item.SetActiveGameObject(true);
        item.transform.SetParent(returnParent);
        items.Remove(item);
        return item;
    }
    
    public void ReturnItem(T item)
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
