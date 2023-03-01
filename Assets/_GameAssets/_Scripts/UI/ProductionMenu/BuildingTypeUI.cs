using TMPro;
using UnityEngine;

public class BuildingTypeUI : EntityTypeUI
{
    [SerializeField] private TextMeshProUGUI _dimensions;

    public void SetData(BuildingType buildingData)
    {
        base.SetData(buildingData);
        _dimensions.text = $"{buildingData.StartWidth}x{buildingData.StartHeight}";
    }
}
