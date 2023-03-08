using System;
using System.Collections;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private Unit _unit;
    private WaitForSeconds _attackWait;
    private WaitForSeconds _halfAttackWait;
    private Coroutine _attackCoroutine;
    private IDamageable _target;
    
    public void Init(Unit unit)
    {
        _unit = unit;
        _attackWait = new WaitForSeconds(1 / _unit.AttackSpeed);
        _halfAttackWait = new WaitForSeconds(1 / (_unit.AttackSpeed * 2));
    }
    
    public void StartAttack(IDamageable to)
    {
        if(to == _target) return;
        _target = to;
        _attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    
    private IEnumerator AttackCoroutine()
    {
        while (_target != null && _target.Health > 0)
        {
            yield return _halfAttackWait;
            _target?.TakeDamage(_unit.Damage);
            yield return _halfAttackWait;
        }
    }
    
    public void StopAttack()
    {
        _target = null;
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }

    private void OnDisable()
    {
        StopAttack();
    }
} 
