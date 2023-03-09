using UnityEngine.Events;

/// <summary>
/// Static public event container.
/// </summary>
public class EventManager : MonoBase
{
    public static UnityEvent<BuildingType> OnBuildingUISelected;
    public static UnityEvent<UnitType> OnUnitUISelected;
    public static UnityEvent<Entity> OnMapEntitySelected;
    public static UnityEvent<CellGrid> OnGridInitialized;
    public static UnityEvent<Team> OnGameEnd;
    
    public static UnityEvent OnClear;
    public static UnityEvent OnNewGame;
    public static UnityEvent OnLoadGame;
    public static UnityEvent OnSaveGame;
    public static UnityEvent OnNextLevel;
    
    public override void Initialize()
    {
        base.Initialize();
        OnBuildingUISelected = new UnityEvent<BuildingType>();
        OnUnitUISelected = new UnityEvent<UnitType>();
        OnMapEntitySelected = new UnityEvent<Entity>();
        OnGridInitialized = new UnityEvent<CellGrid>();
        OnGameEnd = new UnityEvent<Team>();
        
        OnClear = new UnityEvent();
        OnNewGame = new UnityEvent();
        OnLoadGame = new UnityEvent();
        OnSaveGame = new UnityEvent();
        OnNextLevel = new UnityEvent();
    }

    private void OnDestroy()
    {
        OnBuildingUISelected.RemoveAllListeners();
        OnUnitUISelected.RemoveAllListeners();
        OnMapEntitySelected.RemoveAllListeners();
        OnGridInitialized.RemoveAllListeners();
        OnGameEnd.RemoveAllListeners();
        
        OnClear.RemoveAllListeners();
        OnNewGame.RemoveAllListeners();
        OnLoadGame.RemoveAllListeners();
        OnSaveGame.RemoveAllListeners();
        OnNextLevel.RemoveAllListeners();
    }
}