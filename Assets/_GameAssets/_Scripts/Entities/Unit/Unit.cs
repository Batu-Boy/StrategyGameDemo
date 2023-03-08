using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
[RequireComponent(typeof(UnitAttack))]
[RequireComponent(typeof(EnemyDetector))]
public class Unit : Entity
{
    [field: Header("Type Specific")]
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }

    [Header("References")]
    [SerializeField] private UnitMovement _movement;
    [SerializeField] private UnitAttack _attack;
    [SerializeField] private EnemyDetector _enemyDetector;
    
    [Header("Debug")]
    [SerializeField] private Entity _target;

    public override void InitType(EntityType type, Vector2Int position, Team team)
    {
        base.InitType(type, position, team);
        Damage = ((UnitType)type).Damage;
        Range = ((UnitType)type).Range;
        AttackSpeed = ((UnitType)type).AttackSpeed;
        MoveSpeed = ((UnitType)type).MoveSpeed;
        InitComponents();
    }

    private void InitComponents()
    {
        if (!_movement)
            _movement = gameObject.AddComponent<UnitMovement>();
        if (!_attack)
            _attack = gameObject.AddComponent<UnitAttack>();
        if (!_enemyDetector)
            _enemyDetector = gameObject.AddComponent<EnemyDetector>();

        _enemyDetector.Init(this);
        _movement.Init(this);
        _attack.Init(this);

        _enemyDetector.OnTargetDetected += OnTargetInRange;
    }
    
    public void MoveTo(Vector2Int targetPosition)
    {
        ClearTarget();
        _movement.Move(targetPosition);
    }
    
    public void Chase(Entity target)
    {
        if(_target)
            ClearTarget();
        SetTarget(target);

        if (!_enemyDetector.OneStepDetection(_target.transform))
        {
            _enemyDetector.StartDetection(target.transform);
            _movement.Move(target.CurrentPosition);
        }
        else
        {
            OnTargetInRange();
        }
    }
    
    private void OnTargetMove()
    {
        if(_enemyDetector.OneStepDetection(_target.transform)) return;
        
        _attack.StopAttack();
        _enemyDetector.StartDetection(_target.transform);
        _movement.Move(_target.CurrentPosition);
    }
    
    private void OnTargetInRange()
    {
        _movement.StopMovement();
        _attack.StartAttack(_target);
    }

    public void SetTarget(Entity target)
    {
        _target = target;
        _target.onPositionChange += OnTargetMove;
    }

    public void ClearTarget()
    {
        _attack.StopAttack();
        _movement.StopMovement();
        _enemyDetector.StopDetection();
        if(_target)
            _target.onPositionChange -= OnTargetMove;
        _target = null;
    }
    
    protected override void Die()
    {
        EntityDestroyer.DestroyEntity(this);
    }
}