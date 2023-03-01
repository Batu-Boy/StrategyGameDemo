using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProductionMenuViewModel : ScreenElement
{
    [SerializeField] private List<BuildingType> _listingBuildings;
    
    [Header("References")]
    [SerializeField] private BuildingTypeUI _buildingTypeUIPrefab;
    [SerializeField] private RectTransform _nodeParent;

    public override void Initialize()
    {
        base.Initialize();
        ListBuildings();
    }

    private void ListBuildings()
    {
        foreach (var buildingType in _listingBuildings)
        {
            var buildingUI = Instantiate(_buildingTypeUIPrefab, _nodeParent);
            buildingUI.SetData(buildingType);
        }
    }    
}
