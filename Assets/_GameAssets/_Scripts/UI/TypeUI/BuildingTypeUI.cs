using TMPro;
using UnityEngine;

public class BuildingTypeUI : EntityTypeUI
{
    [Header("Building Info")]
    [SerializeField] private TextMeshProUGUI _dimensions;

    public void SetData(BuildingType buildingData)
    {
        base.SetData(buildingData);
        _dimensions.text = $"{buildingData.StartWidth}x{buildingData.StartHeight}";
    }
}