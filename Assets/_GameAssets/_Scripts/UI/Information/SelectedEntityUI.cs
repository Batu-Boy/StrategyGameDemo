using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedEntityUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _health;
    
    public void SetData(Entity entity)
    {
        _icon.sprite = entity.Type.Sprite;
        _name.text = entity.name;
        _health.text = entity.Health.ToString();
    }
}
