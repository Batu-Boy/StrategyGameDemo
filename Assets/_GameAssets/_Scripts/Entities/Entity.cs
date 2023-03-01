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

    public void UpdatePosition(Vector2Int position)
    {
        CurrentPosition = position;
    }
}
