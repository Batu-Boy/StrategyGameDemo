using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class Unit : Entity, IMovable
{
    [field: Header("Type Specific")]
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int Range { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    
    [SerializeField] private UnitMovement _movement;
    
    public override void InitType(EntityType type, Vector2Int position, Team team)
    {
        base.InitType(type, position, team);
        Damage = ((UnitType)type).Damage;
        Range = ((UnitType)type).Range;
        AttackSpeed = ((UnitType)type).AttackSpeed;
        MoveSpeed = ((UnitType)type).MoveSpeed;
        //TODO: not happy with
        _movement.SetMovementSpeed(this);
    }
    
    public void MoveAlong(Path path)
    {
        print("move");
        _movement.Move(path);
    }
    
    public void Attack(Entity to, Path path)
    {
        _movement.Move(path,()=> GiveDamage(to));
    }

    private void GiveDamage(Entity entity)
    {
        //TODO: implement idamageable and damaging with attack speed
    }
}