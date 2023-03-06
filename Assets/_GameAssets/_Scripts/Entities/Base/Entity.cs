using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(EntityVisual))]
public class Entity: MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private EntityVisual _entityVisual;
    
    [Header("Type")]
    public EntityType Type;
    
    [Space] 
    public Team Team;
    public int Health;
    public Vector2Int CurrentPosition;
    
    [EditorButton]
    public virtual void InitType(EntityType type, Vector2Int position, Team team)
    {
        Type = type;
        Health = Type.StartHealth;
        CurrentPosition = position;
        Team = team;
        
        name = $"({Team}){Type.name}";
        transform.position = (Vector3Int)CurrentPosition;
        
        _entityVisual.InitVisual(Type.StartWidth, Type.StartHeight, Type.Sprite);
    }
    
    public void InitSave(EntityType type, Vector2Int position, int health, Team team)
    {
        InitType(type, position, team);
        Health = health;
        //TODO: set and init cell
    }
    
    public void UpdatePosition(Vector2Int position)
    {
        CurrentPosition = position;
    }

    private void OnMouseUpAsButton()
    {
        if(Team == PlayerDataModel.Data.PlayerTeam)
            EventManager.OnMapEntitySelected?.Invoke(this);
    }
}
