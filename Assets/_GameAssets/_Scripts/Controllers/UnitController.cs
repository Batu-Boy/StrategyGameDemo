using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UnitController : MonoBase
{
    [Header("Options")]
    [SerializeField] private LayerMask _entityLayer;
    [SerializeField] private bool _withRaycast = true;
    
    [Header("Debug")]
    [SerializeField] private List<Unit> _selectedUnits;
    
    public override void Initialize()
    {
        base.Initialize();
        _selectedUnits = new List<Unit>();
        EventManager.OnMapEntitySelected.AddListener(SelectUnit);
        EventManager.OnBuildingUISelected.AddListener(OnBuildingUISelect);
    }
    
    private void SelectUnit(Entity entity)
    {
        Deselect();
        if (entity is Unit unit) _selectedUnits.Add(unit);
    }
    
    private void OnBuildingUISelect(BuildingType arg0)
    {
        Deselect();
    }

    private void Update()
    {
        if (_selectedUnits.Count <= 0) return;
        if(!_selectedUnits[0]) Deselect();
        if (!Input.GetMouseButtonDown(1)) return;

        var mouseGridPosition = InputHelper.GetMouseGridPosition();
        if(!GridManager.IsPositionOnGrid(mouseGridPosition)) return;
        if(EventSystem.current.IsPointerOverGameObject()) return;

        if (_withRaycast)
        {
            RaycastControl(mouseGridPosition);
            
        }
        else
        {
            GridPositionControl(mouseGridPosition);
        }
    }

    private void GridPositionControl(Vector2Int mouseGridPosition)
    {
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

    private void RaycastControl(Vector2Int mouseGridPosition)
    {
        var hit = Physics2D.Raycast(mouseGridPosition, Vector2.zero, 100, _entityLayer);
        if (hit)
        {
            if (hit.transform.TryGetComponent<Entity>(out var entity1))
            {
                if (entity1.Team != _selectedUnits[0].Team)
                    Attack(entity1);
            }
        }
        else
        {
            Move(mouseGridPosition);
        }
    }

    private void Attack(Entity enemy)
    {
        _selectedUnits[0].Chase(enemy);
    }

    private void Move(Vector2Int gridPosition)
    {
        _selectedUnits[0].MoveTo(gridPosition);
    }

    private void Deselect() => _selectedUnits.Clear();
}