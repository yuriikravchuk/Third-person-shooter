using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public event Action Died;
    public Vector2 MoveVector { get; set; }

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.Died += Die;
    }

    public void Die() => Died?.Invoke();
}