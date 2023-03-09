using DG.Tweening;
using UnityEngine;
/// <summary>
/// Arranges the visual represent of an entity
/// </summary>
public class EntityVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _graphic;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private Transform _hpScaler;
    [SerializeField] private SpriteRenderer _hpBar;
    
    public void InitVisual(int width, int height, Sprite sprite, Team team)
    {
        bool isWidthEven = width % 2 == 0;
        bool isHeightEven = height % 2 == 0;

        float localX = isWidthEven ? .5f : 0;
        float localY = isHeightEven ? .5f : 0;
        
        ArrangeGraphic(localX, localY, sprite);
        ArrangeCollider(width, height, localX, localY);
        InitTeamColor(team);
    }

    private void ArrangeGraphic(float localX, float localY , Sprite sprite)
    {
        _graphic.localPosition = new Vector3(localX, localY);
        //_graphic.localScale = new Vector3(width, height);
        _spriteRenderer.sprite = sprite;
    }
    
    private void ArrangeCollider(int width, int height,float localX, float localY)
    {
        _collider.offset = new Vector2(localX, localY);
        _collider.size = new Vector2(width, height);
    }
    
    private void InitTeamColor(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                _hpBar.color = Color.blue;
                break;
            case Team.Green:
                _hpBar.color = Color.green;
                break;
            case Team.Red:
                _hpBar.color = Color.red;
                break;
            default:
                _hpBar.color = Color.green;
                break;
        }
    }

    public void UpdateHpVisual(float percentage)
    {
        DOTween.Kill(this);
        _hpScaler.DOScaleX(percentage, .35f).SetId(this);
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}
