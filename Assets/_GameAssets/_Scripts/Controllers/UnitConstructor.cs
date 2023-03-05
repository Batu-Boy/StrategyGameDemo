public class UnitConstructor : MonoBase
{
    private IUnitProducer _selectedProducer;
    
    public override void Initialize()
    {
        base.Initialize();
        InputHelper.Initialize();
        EventManager.OnUnitUISelected.AddListener(OnUnitUISelect);
        EventManager.OnMapEntitySelected.AddListener(OnMapEntitySelect);
    }

    private void OnMapEntitySelect(Entity selectedMapEntity)
    {
        if (selectedMapEntity is IUnitProducer unitProducer)
        {
            _selectedProducer = unitProducer;
        }
        else
        {
            _selectedProducer = null;
        }
    }
    
    private void OnUnitUISelect(UnitType producingUnit)
    {
        _selectedProducer?.Produce(producingUnit);
    }
}
