using UnityEngine;

/// <summary>
/// Type of a unit from scriptable.
/// <seealso cref="EntityType"/>
/// </summary>
[CreateAssetMenu(fileName = "NewUnitType", menuName = "Create Entity Type/New Unit", order = 0)]
public class UnitType : EntityType
{
    [Header("Type Specific")]
    [Range(1,10)] public int Damage;
    [Range(.1f,3)] public float Range;
    [Range(0,3)] public float AttackSpeed;
    [Range(1,3)] public float MoveSpeed;
}