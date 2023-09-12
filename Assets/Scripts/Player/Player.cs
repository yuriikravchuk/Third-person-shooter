using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IDieable
{
    public event Action Died;

    private Health _health;
    
    public void Init(Health health)
    {
        _health = health;
        _health.Died += Die;
    }

    public void TryTakeDamage(int value) => _health.ApplyDamage(value);

    public void Die()
    {
        Died?.Invoke();
    }
}