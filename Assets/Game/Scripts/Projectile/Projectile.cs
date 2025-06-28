using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rBD2;

    private float _damage;

    public void SetUp(Vector2 dir, float speed, float damage)
    {
        _rBD2 = GetComponent<Rigidbody2D>();
        _damage = damage;

        if (_rBD2)
            _rBD2.linearVelocity = dir * speed;

        Destroy(gameObject, 2.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.name);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
