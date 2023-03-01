using TMPro;
using UnityEngine;
using UnityEngine.UI;

//dont want to create an object
public abstract class EntityTypeUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    
    [Space]
    [SerializeReference] public EntityType entityType;
    
    protected void SetData(EntityType entityData)
    {
        entityType = entityData;
        
        _icon.sprite = entityData.Sprite;
        _name.text = entityData.name;
    }
}
