using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _initHealth;

    protected float _currentHealth;

    public virtual void Heal(float amount)
    {
        _currentHealth += amount;
        if(_currentHealth > _initHealth)
            _currentHealth = _initHealth;
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetMaxLife()
    {
        return _initHealth;
    }

    public virtual void OnDie()
    {

    }

    public abstract void TakeDamage(float amount);

    public virtual void Shoot() { }
}
