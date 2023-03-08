using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProductionMenuViewModel : ScreenElement
{
    [SerializeField] private List<BuildingType> _listingBuildings;
    
    [Header("References")]
    [SerializeField] private BuildingTypeUI _buildingTypeUIPrefab;
    [SerializeField] private RectTransform _layoutGroup;
    [SerializeField] private InfiniteScrollView _infiniteScrollView;
    
    public override void Initialize()
    {
        base.Initialize();
        ListBuildings(_infiniteScrollView.GetNecessaryElementCount());
        _infiniteScrollView.Init();
    }
    
    private void ListBuildings(int atLeast)
    {
        
        int iteration = Mathf.CeilToInt((atLeast + 2) / _listingBuildings.Count);

        for (int i = 0; i < iteration; i++)
            foreach (var buildingType in _listingBuildings)
            {
                var buildingUI = Instantiate(_buildingTypeUIPrefab, _layoutGroup);
                buildingUI.SetData(buildingType);
            }
    }
}
