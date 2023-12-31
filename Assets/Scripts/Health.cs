﻿using System;
using UnityEngine;
using UnityEngine.InputSystem.Haptics;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxValue;

    public int Value { get; private set; }
    public bool Damageable { get; set; } = true;
    public event Action Died;
    public event Action<int> ValueChanged;

    public Health(int maxValue)
    {
        _maxValue = maxValue;
        ResetHealth();
    }

    public void TakeDamage(int damage)
    {
        if (Damageable == false) return;
        if (damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));

        Value -= damage;

        ValueChanged?.Invoke(Value);

        if (Value <= 0)
        {
            Value = 0;
            Died.Invoke();
        }
    }

    public void Heal(int healAmount)
    {
        if (healAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(healAmount));

        Value += healAmount;
        if (Value > _maxValue)
            Value = _maxValue;
    }

    public void ResetHealth() => Value = _maxValue;
}