using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Unit _unit;
    [SerializeReference] private Path _currentPath;
    
    private WaitForSeconds _speedWait;
    private Coroutine _movementCoroutine;
    
    public void SetMovementSpeed(Unit unit)
    {
        _unit = unit;
        _moveSpeed = 1;
        _speedWait = new WaitForSeconds(_moveSpeed);
    }
    
    public void Move(Path path)
    {
        StopMovement();
        _currentPath = path;
        _movementCoroutine = StartCoroutine(MovementCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        while (_currentPath.wayPoints.Count != 0)
        {
            var nextTargetPoint = _currentPath.GetNextPoint();
            transform.DOMove(nextTargetPoint, _moveSpeed).SetEase(Ease.Linear).SetId(this);
            yield return _speedWait;
            _unit.CurrentPosition = nextTargetPoint.ToGridPos();
        }
        
        if (_currentPath.IsComplete)
        {
            Debug.Log($"Path Complete on:{name}");
            _currentPath = null;
        }
    }

    private void StopMovement()
    {
        _currentPath = null;
        DOTween.Kill(this);
        if(_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);
    }
}
