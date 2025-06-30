using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rBD2;

    private float _damage;

    public void SetUp(Vector2 dir, float speed, float damage, float timeToDestroy)
    {
        _rBD2 = GetComponent<Rigidbody2D>();
        _damage = damage;

        if (_rBD2)
            _rBD2.linearVelocity = dir * speed;

        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity entity = collision.gameObject.GetComponent<Entity>();
        if (entity)
        {
            float angle = transform.rotation.eulerAngles.z;
            ParticleActions.CreateHurtPTC(collision.GetContact(0).point, transform.rotation, PTC_TYPE.HURT);
            entity.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
