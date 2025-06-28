using System.Collections;
using UnityEngine;

public class BowBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private float _coolDown;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _timeToDestroyProjectile;

    [SerializeField]
    private Transform _projectilePool;

    private Vector2 _dir;

    private Coroutine _shootCoroutine;

    public void Shoot(Vector2 dir)
    {
        if (_shootCoroutine == null)
        {
            _dir = dir;
            _shootCoroutine = StartCoroutine(nameof(ShootProjectile));
        }
    }

    private IEnumerator ShootProjectile()
    {
        GameObject currentProjectile = Instantiate(_projectilePrefab, _projectilePool);
        currentProjectile.transform.position = transform.position;

        // Angle of the z axis (2D Game)
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        currentProjectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        currentProjectile.GetComponent<Projectile>().SetUp(_dir, _projectileSpeed, _damage, _timeToDestroyProjectile);

        yield return new WaitForSecondsRealtime(_coolDown);
        _shootCoroutine = null;
    }

}
