using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UnitProducer))]
public class Building : Entity
{
    [Header("Type Specific")]
    [SerializeField] private UnitProducer _producer;
    
    public override void InitType(EntityType type, Vector2Int position, Team team)
    {
        base.InitType(type, position, team);
        _producer.CalculateSpawnPoint(type.StartHeight, position);
    }
    
    public void Produce(UnitType unitType)
    {
        _producer.ProduceUnit(unitType, Team);
    }
    
    protected override void DeInit()
    {
        //
    }

    protected override void Die()
    {
        EntityDestroyer.DestroyEntity(this);
    }
}
