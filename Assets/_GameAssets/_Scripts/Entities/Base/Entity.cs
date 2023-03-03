using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity: MonoBehaviour
{
    [Header("Entity Settings")]
    public int Health;
    public Vector2Int CurrentPosition;
    [FormerlySerializedAs("type")] public EntityType Type;
    
    [Header("References")]
    [SerializeField] private Transform _graphic;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _collider;

    public virtual void InitType(EntityType type,Vector2Int position)
    {
        Type = type;
        Health = Type.StartHealth;
        CurrentPosition = position;
        
        name = Type.name;
        transform.position = (Vector3Int)CurrentPosition;

        ArrangeGraphic(Type.StartWidth,Type.StartHeight);
        ArrangeCollider(Type.StartWidth,Type.StartHeight);
    }

    public void InitSave(EntityType type, Vector2Int position, int health)
    {
        InitType(type, position);
        Health = health;
        //TODO: set and init cell
    }
    
    private void ArrangeGraphic(int width, int height)
    {
        _graphic.localPosition = new Vector3(width / 2f, height / 2f);
        _spriteRenderer.sprite = Type.Sprite;
    }

    private void ArrangeCollider(int width, int height)
    {
        _collider.size = new Vector2(width, height);
        _collider.offset = new Vector2(width / 2f, height / 2f);
    }
    
    public void UpdatePosition(Vector2Int position)
    {
        CurrentPosition = position;
    }
    
    private void OnMouseUpAsButton()
    {
        print("map click");
        EventManager.OnMapEntitySelected?.Invoke(this);
    }
}
