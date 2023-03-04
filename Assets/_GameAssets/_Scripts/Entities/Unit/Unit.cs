using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class Unit : Entity
{
    [Header("Type Specific")]
    public UnitMovement Movement;
    
    public override void InitType(EntityType type, Vector2Int position)
    {
        base.InitType(type, position);
        
    }
} 
