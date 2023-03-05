using System.Collections.Generic;
using UnityEngine;

public class InformationViewModel : ScreenElement
{
    [Header("References")]
    [SerializeField] private SelectedEntityUI _selectedEntityUI;
    [SerializeField] private List<UnitTypeUI> _unitTypeUIs;
    
    [Header("Debug")]
    [SerializeField] private Entity currentShowingEntity;
    
    public override void Initialize()
    {
        base.Initialize();
        ClearInfo();
        EventManager.OnMapEntitySelected.AddListener(ShowInfo);
    }
    
    public void ShowInfo(Entity entity)
    {
        if(currentShowingEntity == entity) return;
        
        ClearInfo();
        currentShowingEntity = entity;
        _selectedEntityUI.SetData(entity);
        _selectedEntityUI.SetActiveGameObject(true);
        
        if (entity.Type is not BuildingType buildingType) return;
        if(buildingType.Productions == null || buildingType.Productions.Count < 0 ) return;
        
        ListProductions(buildingType.Productions);
    }
    
    private void ListProductions(List<EntityType> buildingProductions)
    {
        for (var index = 0; index < buildingProductions.Count; index++)
        {
            var buildingProduction = buildingProductions[index];
            _unitTypeUIs[index].SetData(buildingProduction as UnitType);
            _unitTypeUIs[index].SetActiveGameObject(true);
        }
    }
    
    private void ClearInfo()
    {
        foreach (var unitTypeUI in _unitTypeUIs)
        {
            unitTypeUI.SetActiveGameObject(false);
        }
        
        currentShowingEntity = null;
        _selectedEntityUI.SetActiveGameObject(false);
    }
}