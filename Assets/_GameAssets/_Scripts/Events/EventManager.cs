using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBase
{
    public static UnityEvent<BuildingType> OnBuildingUISelected;
    public static UnityEvent<UnitType> OnUnitUISelected;
    public static UnityEvent<Entity> OnMapEntitySelected;
    public static UnityEvent<CellGrid> OnGridInitialized;
    public static UnityEvent OnClear;
    public static UnityEvent OnNewGame;
    public static UnityEvent OnLoadGame;
    public static UnityEvent OnSaveGame;

    public override void Initialize()
    {
        base.Initialize();
        OnBuildingUISelected = new UnityEvent<BuildingType>();
        OnUnitUISelected = new UnityEvent<UnitType>();
        OnMapEntitySelected = new UnityEvent<Entity>();
        OnGridInitialized = new UnityEvent<CellGrid>();
        OnClear = new UnityEvent();
        OnNewGame = new UnityEvent();
        OnLoadGame = new UnityEvent();
        OnSaveGame = new UnityEvent();
    }

    private void OnDestroy()
    {
        OnBuildingUISelected.RemoveAllListeners();
        OnUnitUISelected.RemoveAllListeners();
        OnMapEntitySelected.RemoveAllListeners();
        OnGridInitialized.RemoveAllListeners();
        OnClear.RemoveAllListeners();
        OnNewGame.RemoveAllListeners();
        OnLoadGame.RemoveAllListeners();
        OnSaveGame.RemoveAllListeners();
    }
}