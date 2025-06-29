using System.Collections;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    [SerializeField]
    protected int _damage;

    [SerializeField]
    protected float _coolDown;

    protected Vector2 _dir;

    protected Coroutine _attackCoroutine;

    public virtual void Attack(Vector2 dir)
    {
        if (_attackCoroutine == null)
        {
            _dir = dir;
            _attackCoroutine = StartCoroutine(nameof(AttackCoroutine));
        }
    }

    protected virtual IEnumerator AttackCoroutine()
    {
        yield return null;
    }
}
