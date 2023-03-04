using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(UnitProducer))]
public class Building : Entity
{
    [Header("Type Specific")]
    [SerializeField] private UnitProducer _producer;
    
    public override void InitType(EntityType type, Vector2Int position)
    {
        base.InitType(type, position);
        _producer.CalculateSpawnPoint(type.StartHeight, position);
    }
    
    public void ProduceUnit(UnitType unitType)
    {
        _producer.ProduceUnit(unitType);
    }
}
