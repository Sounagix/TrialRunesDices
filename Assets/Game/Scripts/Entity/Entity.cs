using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected int _initHealth;

    protected int _currentHealth;

    public virtual void Heal(int amount)
    {
        _currentHealth += amount;
        if(_currentHealth > _initHealth)
            _currentHealth = _initHealth;
    }

    public void TakeDamage(int amount)
    {
        _initHealth -= amount;
        if (_initHealth <= 0)
            print("Death");
    }
}
