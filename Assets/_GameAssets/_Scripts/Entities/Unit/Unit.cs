using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class Unit : Entity, IMovable
{
    [Header("Type Specific")]
    [SerializeField] private UnitMovement _movement;
    
    public override void InitType(EntityType type, Vector2Int position, Team team)
    {
        base.InitType(type, position, team);
        //TODO: not happy with
        _movement.SetMovementSpeed(this);
    }
    
    public void MoveAlong(Path path)
    {
        _movement.Move(path);
    }
}