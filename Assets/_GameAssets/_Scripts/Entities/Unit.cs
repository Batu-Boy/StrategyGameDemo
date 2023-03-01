using UnityEngine;

public class Unit : Entity
{
    public int Damage;
    public int Range;
    public float AttackSpeed;
    
    public override void InitType(EntityType type,Vector2Int position)
    {
        base.InitType(type,position);
        
        if (Type is not UnitType unitType)
        {
            Debug.LogError($"Type Casting Error! [{name}]");
            return;
        }
        
        Damage = unitType.Damage;
        Range = unitType.Range;
        AttackSpeed = unitType.AttackSpeed;
    }
}