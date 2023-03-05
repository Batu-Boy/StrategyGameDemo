using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementController : MonoBase
{
    [SerializeField] private List<Unit> _selectedMovables;

    private PathFinder _pathFinder;
    public override void Initialize()
    {
        base.Initialize();
        _selectedMovables = new List<Unit>();
        _pathFinder = GridManager.PathFinder;
        EventManager.OnMapEntitySelected.AddListener(OnMapEntitySelect);
    }

    private void OnMapEntitySelect(Entity entity)
    {
        _selectedMovables.Clear();
        if (entity is Unit movable)
        {
            _selectedMovables.Add(movable);
        }
    }

    private void Update()
    {
        if(_selectedMovables.Count <= 0) return;

        if (Input.GetMouseButtonDown(1))
        {
            var mouseGridPosition = InputHelper.GetMouseGridPosition();
            if (GridManager.IsPositionValid(mouseGridPosition))
            {
                var path = _pathFinder.FindPath(_selectedMovables[0].CurrentPosition, mouseGridPosition);
                _selectedMovables[0].MoveAlong(path);
            }
        }
    }
}