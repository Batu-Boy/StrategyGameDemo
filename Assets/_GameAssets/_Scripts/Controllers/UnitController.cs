using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBase
{
    [SerializeField] private List<Unit> _selectedUnits;
    
    public override void Initialize()
    {
        base.Initialize();
        _selectedUnits = new List<Unit>();
        EventManager.OnMapEntitySelected.AddListener(SelectUnit);
        EventManager.OnBuildingUISelected.AddListener(Deselect);
    }
    
    private void SelectUnit(Entity entity)
    {
        _selectedUnits.Clear();
        if (entity is Unit movable) _selectedUnits.Add(movable);
    }
    
    private void Deselect(BuildingType arg0)
    {
        _selectedUnits.Clear();
    }
    
    private void Update()
    {
        if (_selectedUnits.Count <= 0) return;

        if (!Input.GetMouseButtonDown(1)) return;

        var mouseGridPosition = InputHelper.GetMouseGridPosition();
        if(!GridManager.IsPositionOnGrid(mouseGridPosition)) return;
        
        if (GridManager.TryGetEntityOnCell(mouseGridPosition, out var entity))
        {
            if (entity.Team != _selectedUnits[0].Team)
                Attack(entity);
        }
        else
        {
            Move(mouseGridPosition);
        }
    }
    
    private void Attack(Entity to)
    {
        _selectedUnits[0].Chase(to);
    }
    
    private void Move(Vector2Int gridPosition)
    {
        _selectedUnits[0].MoveTo(gridPosition);
    }
}