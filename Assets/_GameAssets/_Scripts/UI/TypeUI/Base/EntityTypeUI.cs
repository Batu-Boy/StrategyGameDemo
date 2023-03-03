using TMPro;
using UnityEngine;
using UnityEngine.UI;

//dont want to create an object
public abstract class EntityTypeUI : MonoBehaviour
{
    [Header("EntityInfo")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    
    [Space]
    [SerializeReference] public EntityType entityType;
    
    public void SetData(EntityType entityData)
    {
        entityType = entityData;
        
        _icon.sprite = entityData.Sprite;
        _name.text = entityData.name;
    }
    
    private void OnMouseUpAsButton()
    {
        EventManager.OnEntityUISelected?.Invoke(entityType);
    }
}