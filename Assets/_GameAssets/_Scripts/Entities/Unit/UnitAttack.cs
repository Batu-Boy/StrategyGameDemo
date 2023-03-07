using System.Collections;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    private Unit _unit;
    private WaitForSeconds _attackWait;
    private WaitForSeconds _halfAttackWait;
    private Coroutine _attackCoroutine;
    
    public void Init(Unit unit)
    {
        _unit = unit;
        _attackWait = new WaitForSeconds(1 / _unit.AttackSpeed);
        _halfAttackWait = new WaitForSeconds(1 / (_unit.AttackSpeed * 2));
    }
    
    public void StartAttack(IDamageable to)
    {
        _attackCoroutine = StartCoroutine(AttackCoroutine(to));
    }

    private IEnumerator AttackCoroutine(IDamageable to)
    {
        while (to.Health > 0)
        {
            yield return _halfAttackWait;
            to.TakeDamage(_unit.Damage);
            Debug.Log($"{name} attacking to:{to}");
            yield return _halfAttackWait;
        }
    }

    public void StopAttack()
    {
        if(_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
    }
} 
