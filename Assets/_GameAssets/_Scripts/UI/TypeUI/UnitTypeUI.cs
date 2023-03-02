using TMPro;
using UnityEngine;

public class UnitTypeUI : EntityTypeUI
{
    [Header("Unit Info")] 
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _attackSpeed;
    
    public void SetData(UnitType productData)
    {
        base.SetData(productData);
        _health.text = productData.StartHealth.ToString();
        _damage.text = productData.Damage.ToString();
        _attackSpeed.text = productData.AttackSpeed.ToString();
    }
}
