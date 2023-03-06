using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public bool IsMoving => _currentPath != null;
    
    private Unit _unit;
    private Path _currentPath;
    private Coroutine _movementCoroutine;
    
    public void SetMovementSpeed(Unit unit)
    {
        _unit = unit;
    }
    
    public void Move(Path path)
    {
        StopMovement();
        _currentPath = path;
        _movementCoroutine = StartCoroutine(MovementCoroutine());
    }
    
    public void Move(Path path, Action onComplete)
    {
        StopMovement();
        _currentPath = path;
        _movementCoroutine = StartCoroutine(MovementCoroutine(onComplete));
    }
    
    private IEnumerator MovementCoroutine(Action onComplete)
    {
        while (_currentPath.WayPoints.Count != 0)
        {
            var nextTargetPoint = _currentPath.GetNextPoint();
            float distance = Vector3.Distance(nextTargetPoint, transform.position);
            float duration = distance / _unit.MoveSpeed;
            transform.DOMove(nextTargetPoint, duration).SetEase(Ease.Linear).SetId(this);
            yield return new WaitForSeconds(duration);
            _unit.UpdatePosition(nextTargetPoint.ToGridPos());
        }
        
        if (_currentPath.IsComplete)
        {
            onComplete?.Invoke();
            _currentPath = null;
        }
    }
    
    private IEnumerator MovementCoroutine()
    {
        while (_currentPath.WayPoints.Count != 0)
        {
            var nextTargetPoint = _currentPath.GetNextPoint();
            float distance = Vector3.Distance(nextTargetPoint, transform.position);
            float duration = distance / _unit.MoveSpeed;
            transform.DOMove(nextTargetPoint, duration).SetEase(Ease.Linear).SetId(this);
            yield return new WaitForSeconds(duration);
            _unit.UpdatePosition(nextTargetPoint.ToGridPos());
        }
        
        if (_currentPath.IsComplete)
        {
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
