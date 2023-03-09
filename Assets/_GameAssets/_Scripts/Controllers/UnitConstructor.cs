/// <summary>
/// Controls unit construction while checking input for unit ui clicks on information menu
/// </summary>
public class UnitConstructor : MonoBase
{
    private Building _selectedProducer;
    
    public override void Initialize()
    {
        base.Initialize();
        InputHelper.Initialize();
        EventManager.OnUnitUISelected.AddListener(OnUnitUISelect);
        EventManager.OnMapEntitySelected.AddListener(OnMapEntitySelect);
    }

    private void OnMapEntitySelect(Entity selectedMapEntity)
    {
        if (selectedMapEntity is Building unitProducer)
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
        if(_selectedProducer)
            _selectedProducer.Produce(producingUnit);
    }
}
