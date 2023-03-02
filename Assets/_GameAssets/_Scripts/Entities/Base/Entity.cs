using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity: MonoBehaviour
{
    [Header("Entity Settings")]
    public int Width;
    public int Height;
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
        Width = Type.StartWidth;
        Height = Type.StartHeight;
        Health = Type.StartHealth;
        CurrentPosition = position;
        
        name = Type.name;
        transform.position = (Vector3Int)CurrentPosition;

        ArrangeGraphic();
        ArrangeCollider();
    }

    public void InitSave(EntityType type, Vector2Int position, int health)
    {
        InitType(type, position);
        Health = health;
        //TODO: set and init cell
    }
    
    private void ArrangeGraphic()
    {
        _graphic.localPosition = new Vector3(Width / 2f, Height / 2f);
        _spriteRenderer.sprite = Type.Sprite;
    }
    
    private void ArrangeCollider()
    {
        _collider.size = new Vector2(Width, Height);
        _collider.offset = new Vector2(Width / 2f, Height / 2f);
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
