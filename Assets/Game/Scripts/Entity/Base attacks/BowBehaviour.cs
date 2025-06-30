using System;
using System.Collections;
using UnityEngine;

public class BowBehaviour : BaseAttack
{
    [Header("Range Attack")]

    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _timeToDestroyProjectile;

    [SerializeField]
    private Transform _projectilePool;


    protected override IEnumerator AttackCoroutine()
    {
        GameObject currentProjectile = Instantiate(_projectilePrefab, _projectilePool);
        currentProjectile.transform.position = transform.position;

        // Angle of the z axis (2D Game)
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        currentProjectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        currentProjectile.GetComponent<Projectile>().SetUp(_dir, _projectileSpeed, _damage, _timeToDestroyProjectile);

        yield return new WaitForSecondsRealtime(_coolDown);
        _attackCoroutine = null;
    }
}
