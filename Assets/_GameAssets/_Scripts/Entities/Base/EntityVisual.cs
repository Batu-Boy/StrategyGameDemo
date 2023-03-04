using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _graphic;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _collider;
    
    public void InitVisual(int width, int height, Sprite sprite)
    {
        ArrangeGraphic(width, height, sprite);
        ArrangeCollider(width, height);
    }
    
    private void ArrangeGraphic(int width, int height , Sprite sprite)
    {
        bool isWidthEven = width % 2 == 0;
        bool isHeightEven = height % 2 == 0;

        float localX = isWidthEven ? .5f : 0;
        float localY = isHeightEven ? .5f : 0;
        
        _graphic.localPosition = new Vector3(localX, localY);
        //_graphic.localScale = new Vector3(width, height);
        _spriteRenderer.sprite = sprite;
    }
    
    private void ArrangeCollider(int width, int height)
    {
        _collider.size = new Vector2(width, height);
    }
}
