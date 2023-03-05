using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBase
{
    public static UnityEvent<BuildingType> OnBuildingUISelected;
    public static UnityEvent<UnitType> OnUnitUISelected;
    public static UnityEvent<Entity> OnMapEntitySelected;
    public static UnityEvent<CellGrid> OnGridInitialized;
    public static UnityEvent OnClear;
    
    public override void Initialize()
    {
        base.Initialize();
        OnBuildingUISelected = new UnityEvent<BuildingType>();
        OnUnitUISelected = new UnityEvent<UnitType>();
        OnMapEntitySelected = new UnityEvent<Entity>();
        OnGridInitialized = new UnityEvent<CellGrid>();
        OnClear = new UnityEvent();
    }

    private void OnDestroy()
    {
        OnBuildingUISelected.RemoveAllListeners();
        OnUnitUISelected.RemoveAllListeners();
        OnMapEntitySelected.RemoveAllListeners();
        OnGridInitialized.RemoveAllListeners();
        OnClear.RemoveAllListeners();
    }
}