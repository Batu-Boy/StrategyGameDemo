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
        if (GridManager.TryGetEntityOnCell(mouseGridPosition, out var entity))
        {
            if (entity.Team != PlayerDataModel.Data.PlayerTeam)
                Attack(entity);
        }
        else
        {
            Move(mouseGridPosition);
        }
    }
    
    private void Attack(Entity to)
    {
        var path = GridManager.PathFinder.FindPath(_selectedUnits[0].CurrentPosition, to.CurrentPosition);
        if(path == null) return;
        path.RemovePointFromEnd(_selectedUnits[0].Range);
        if(path.WayPoints.Count > 0)
            _selectedUnits[0].MoveAlong(path);
    }
    
    private void Move(Vector2Int gridPosition)
    {
        var path = GridManager.PathFinder.FindPath(_selectedUnits[0].CurrentPosition, gridPosition);
        if(path != null && path.WayPoints.Count > 0)
            _selectedUnits[0].MoveAlong(path);
    }
}