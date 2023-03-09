using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Type of a building from scriptable.
/// <seealso cref="EntityType"/>
/// </summary>
[CreateAssetMenu(fileName = "NewBuildingType", menuName = "Create Entity Type/New Building", order = 1)]
public class BuildingType : EntityType
{
    /// <summary>
    /// Productions with entity type
    /// </summary>
    [Header("Type Specific")]
    [SerializeReference] public List<EntityType> Productions = new ();
}