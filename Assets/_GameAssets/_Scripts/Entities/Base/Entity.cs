using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EntityVisual))]
public class Entity: MonoBehaviour, IPointerClickHandler
{
    [Header("References")] 
    [SerializeField] private EntityVisual _entityVisual;
    
    [Header("Type")]
    public EntityType Type;
    
    [Space]
    public int Health;
    public Vector2Int CurrentPosition;
    
    [EditorButton]
    public virtual void InitType(EntityType type,Vector2Int position)
    {
        Type = type;
        Health = Type.StartHealth;
        CurrentPosition = position;
        
        name = Type.name;
        transform.position = (Vector3Int)CurrentPosition;

        _entityVisual.InitVisual(Type.StartWidth, Type.StartHeight, Type.Sprite);
    }
    
    public void InitSave(EntityType type, Vector2Int position, int health)
    {
        InitType(type, position);
        Health = health;
        //TODO: set and init cell
    }
    
    public void UpdatePosition(Vector2Int position)
    {
        CurrentPosition = position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        EventManager.OnMapEntitySelected?.Invoke(this);
    }
}
