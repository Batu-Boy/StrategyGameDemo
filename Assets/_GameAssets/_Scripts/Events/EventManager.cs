using UnityEngine.Events;

public class EventManager : MonoBase
{
    public static UnityEvent<EntityType> OnEntityUISelected;
    public static UnityEvent<Entity> OnMapEntitySelected;

    public override void Initialize()
    {
        base.Initialize();
        OnEntityUISelected = new UnityEvent<EntityType>();
        OnMapEntitySelected = new UnityEvent<Entity>();
    }

    private void OnDestroy()
    {
        OnEntityUISelected.RemoveAllListeners();
        OnMapEntitySelected.RemoveAllListeners();
    }
}