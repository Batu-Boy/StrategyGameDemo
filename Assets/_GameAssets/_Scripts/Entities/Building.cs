using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    [SerializeReference] public List<EntityType> Productions = new();
    
    public override void InitType(EntityType type,Vector2Int position)
    {
        base.InitType(type,position);

        if (Type is not BuildingType buildingType)
        {
            Debug.LogError($"Type Casting Error! [{name}]");
            return;
        }

        Productions = buildingType.Productions;
    }
}
