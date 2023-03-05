using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class UnitController : MonoBase
{
    [SerializeField] private List<Unit> _selectedUnits;

    private Camera cam;
    private PathFinder _pathFinder;
    
    public override void Initialize()
    {
        base.Initialize();
        cam = Camera.main;
        _selectedUnits = new List<Unit>();
        _pathFinder = GridManager.PathFinder;
        EventManager.OnMapEntitySelected.AddListener(SelectUnit);
        EventManager.OnBuildingUISelected.AddListener(Deselect);
    }
    
    private void SelectUnit(Entity entity)
    {
        _selectedUnits.Clear();
        if (entity is Unit movable)
        {
            _selectedUnits.Add(movable);
        }
    }
    
    private void Deselect(BuildingType arg0)
    {
        _selectedUnits.Clear();
    }
    
    private void Update()
    {
        if(_selectedUnits.Count <= 0) return;

        if (!Input.GetMouseButtonDown(1)) return;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -1;
        Vector3 screenPos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawLine (screenPos, screenPos + Vector3.forward * 10, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(screenPos,Vector2.zero);
        if(hit)
        {
            print("hit");
            if (hit.transform.TryGetComponent(out Entity entity))
            {
                print("entity");
                if(entity.Team != PlayerDataModel.Data.PlayerTeam)
                    Attack(entity);
            }
        }
        else
        {
            Move();
        }
    }
    
    private void Attack(Entity to)
    {
        Debug.LogWarning($"{_selectedUnits[0].name} Attacking to:{to.name}");
    }
    
    private void Move()
    {
        var mouseGridPosition = InputHelper.GetMouseGridPosition();
        if (GridManager.IsPositionValid(mouseGridPosition))
        {
            var path = _pathFinder.FindPath(_selectedUnits[0].CurrentPosition, mouseGridPosition);
            _selectedUnits[0].MoveAlong(path);
        }
    }
}