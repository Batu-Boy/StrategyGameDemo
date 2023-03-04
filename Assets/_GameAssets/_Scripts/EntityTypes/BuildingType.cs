using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBuildingType", menuName = "Create Entity Type/New Building", order = 1)]
public class BuildingType : EntityType
{
    [Header("Type Specific")]
    [SerializeReference] public List<EntityType> Productions = new ();
}