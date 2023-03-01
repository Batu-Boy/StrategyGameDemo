using System.Collections.Generic;
using UnityEngine;

public class InformationViewModel : ScreenElement
{
    [SerializeField] private SelectedEntityUI _selectedEntityUI;
    [SerializeField] private Transform _nodeParent;

    public override void Initialize()
    {
        base.Initialize();
        
    }

    public void ShowInfo(Entity entity)
    {
        _selectedEntityUI.SetData(entity);

        if (entity is not Building building) return;
        if(building.Productions == null || building.Productions.Count < 0 ) return;

        ListProductions(building.Productions);
    }

    private void ListProductions(List<EntityType> buildingProductions)
    {
        
    }

    public void ClearInfo()
    {
        
    }
}
