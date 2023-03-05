using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//dont want to create an object
public abstract class EntityTypeUI<T> : MonoBehaviour
    where T : EntityType
{
    [Space]
    [SerializeReference] public T entityType;
    
    [Header("EntityInfo")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;

    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public virtual void SetData(T entityData)
    {
        entityType = entityData;
        
        _icon.sprite = entityData.Sprite;
        _name.text = entityData.name;
    }
    
    protected abstract void OnClick();
}