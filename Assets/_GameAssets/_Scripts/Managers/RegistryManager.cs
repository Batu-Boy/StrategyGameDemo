using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

//TODO: save version
public class RegistryManager : MonoBase
{
    public static List<Entity> RegisteredEntities { get; private set; }

    public override void Initialize()
    {
        base.Initialize();
        
        RegisteredEntities = new List<Entity>();
    }

    public static void RegisterEntity(Entity entity)
    {
        if(!RegisteredEntities.Contains(entity))
            RegisteredEntities.Add(entity);
    }
    
    public static void RemoveEntity(Entity entity)
    {
        RegisteredEntities.Remove(entity);
    }

    public static void ClearEntities()
    {
        RegisteredEntities.Clear();
    }

}