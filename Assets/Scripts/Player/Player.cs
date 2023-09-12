using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IDieable
{
    [SerializeField] private int _maxHealth = 100;

    public event Action Died;

    private Health _health;
    
    public void Awake()
    {
        _health = new Health(_maxHealth);
        _health.Died += Die;
    }

    public void TryTakeDamage(int value) => _health.ApplyDamage(value);

    public void Die()
    {
        Died?.Invoke();
    }
}