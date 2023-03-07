using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private bool gizmos;
    
    private Unit _unit;
    private Path _currentPath;
    private Coroutine _movementCoroutine;
    
    public void Init(Unit unit)
    {
        _unit = unit;
    }
    
    public void Move(Vector2Int position, Action onComplete = null)
    {
        StopMovement();
        _currentPath = GetPath(_unit.CurrentPosition, position);
        _movementCoroutine = StartCoroutine(MovementCoroutine(_currentPath, onComplete));
    }
    
    private IEnumerator MovementCoroutine(Path path,Action onComplete = null)
    {
        while (path.WayPoints.Count != 0)
        {
            var nextTargetPoint = path.GetNextPoint();
            float distance = Vector3.Distance(nextTargetPoint, transform.position);
            float duration = distance / _unit.MoveSpeed;
            transform.DOMove(nextTargetPoint, duration).SetEase(Ease.Linear).SetId(this);
            yield return new WaitForSeconds(duration);
            _unit.UpdatePosition(nextTargetPoint.ToGridPos());
        }
        
        if (path.IsComplete)
        {
            onComplete?.Invoke();
        }
    }

    private Path GetPath(Vector2Int from,Vector2Int targetPosition)
    {
        return GridManager.PathFinder.FindPath(from, targetPosition);
    }
    
    public void StopMovement()
    {
        _currentPath = null;
        if(_movementCoroutine != null)
            StopCoroutine(_movementCoroutine);
        DOTween.Kill(this);
    }

    private void OnDrawGizmos()
    {
        if(!gizmos) return;
        
        if(_currentPath == null) return;

        if(_currentPath.WayPoints.Count <= 1) return;
        
        Gizmos.color = Color.green;
        for (int i = 0; i < _currentPath.WayPoints.Count - 2; i++)
        {
            Gizmos.DrawLine(_currentPath.WayPoints[i].ToMapPos(), _currentPath.WayPoints[i + 1].ToMapPos());
        }
    }
}
