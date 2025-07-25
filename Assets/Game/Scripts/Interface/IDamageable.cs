using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float amount);
    void Heal(float amount);
}
