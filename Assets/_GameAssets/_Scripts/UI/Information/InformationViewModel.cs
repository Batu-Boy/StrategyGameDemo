using System.Collections.Generic;
using UnityEngine;

public class InformationViewModel : ScreenElement
{
    [Header("References")]
    [SerializeField] private SelectedEntityUI _selectedEntityUI;
    [SerializeField] private List<UnitTypeUI> _unitTypeUIs;
    
    [Header("Debug")]
    [SerializeField] private Entity _currentShowingEntity;
    
    public override void Initialize()
    {
        base.Initialize();
        ClearInfo();
        EventManager.OnMapEntitySelected.AddListener(ShowInfo);
    }
    
    [EditorButton]
    public void ShowInfo(Entity entity)
    {
        if(_currentShowingEntity == entity) return;
        
        ClearInfo();
        _currentShowingEntity = entity;
        _selectedEntityUI.SetData(entity);
        
        if (entity is not Building building) return;
        if(building.Productions == null || building.Productions.Count < 0 ) return;
        
        ListProductions(building.Productions);
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

        _currentShowingEntity = null;
    }
}
