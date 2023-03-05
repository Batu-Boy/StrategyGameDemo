using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Unit _unit;
    private Path _currentPath;
    
    private Coroutine _movementCoroutine;
    
    public void SetMovementSpeed(Unit unit)
    {
        _unit = unit;
        _moveSpeed = 3;
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
            float distance = Vector3.Distance(nextTargetPoint, transform.position);
            float duration = distance / _moveSpeed;
            transform.DOMove(nextTargetPoint, duration).SetEase(Ease.Linear).SetId(this);
            yield return new WaitForSeconds(duration);
            _unit.CurrentPosition = nextTargetPoint.ToGridPos();
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
