using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected float _initHealth;

    protected float _currentHealth;

    public virtual void Heal(int amount)
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

    public abstract void TakeDamage(int amount);
}
