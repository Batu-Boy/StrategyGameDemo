using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDetector : MonoBehaviour
{
    [SerializeField] private float _detectFrequency;
    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private bool gizmos;

    public Action OnTargetDetected;

    private int _enemyCount;
    private float _detectionRange;
    private Transform _target;
    private WaitForSeconds _detectWait;
    private Coroutine _detectionCoroutine;
    private Collider2D[] _enemies = new Collider2D[30];
    
    public void Init(Unit unit)
    {
        _detectWait = new WaitForSeconds(_detectFrequency);
        _detectionRange = unit.Range + .5f;
    }
    
    private IEnumerator Detection(Transform target)
    {
        if(!target) yield break;
        _enemyCount = Physics2D.OverlapCircleNonAlloc(transform.position, _detectionRange, _enemies, _targetLayer);
        if(CheckTargetReached(target)) yield break;
        
        yield return _detectWait;
        
        if(!target) yield break;
        _detectionCoroutine = StartCoroutine(Detection(target));
    }
    
    private bool CheckTargetReached(Transform target)
    {
        if (_enemyCount <= 1) return false;
        
        for (int i = 0; i < _enemyCount; i++)
        {
            if (_enemies[i].transform == target)
            {
                OnTargetDetected?.Invoke();
                StopDetection();
                return true;
            }
        }
        return false;
    }
    
    public void StartDetection(Transform target)
    {
        _detectionCoroutine = StartCoroutine(Detection(target));
    }

    public void StopDetection()
    {
        _enemyCount = 0;
        if(_detectionCoroutine != null)
            StopCoroutine(_detectionCoroutine);
    }

    public Transform GetClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        Transform closestEnemy = null;
        for (int i = 0; i < _enemyCount; i++)
        {
            if (Vector3.Distance(_enemies[i].transform.position, transform.position) < closestDistance)
            {
                if(_enemies[i].transform != transform)
                    closestEnemy = _enemies[i].transform;
            }
        }

        return closestEnemy;
    }
    
    private void OnDrawGizmos()
    {
        if(!gizmos) return;
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
        for (int i = 0; i < _enemyCount; i++)
        {
            Gizmos.color = Color.red;
            if(_enemies[i].transform != transform)
                Gizmos.DrawLine(transform.position, _enemies[i].transform.position);
        }
    }
}