using TMPro;
using UnityEngine;

public class BuildingTypeUI : EntityTypeUI<BuildingType>
{
    [Header("Building Info")]
    [SerializeField] private TextMeshProUGUI _dimensions;
    
    public override void SetData(BuildingType buildingData)
    {
        base.SetData(buildingData);
        _dimensions.text = $"{buildingData.StartWidth}x{buildingData.StartHeight}";
    }
    
    protected override void OnClick()
    {
        EventManager.OnBuildingUISelected?.Invoke(entityType);
    }
}