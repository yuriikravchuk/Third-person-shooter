using System;
public class Health
{
    public float Value { get; private set; }
    public bool Damageable { get; set; } = true;
    public event Action Died;

    private readonly int _maxValue;

    public Health(int maxValue) 
        => _maxValue = maxValue;

    public void ApplyDamage(int damage)
    {
        if (Damageable == false) return;
        if (damage < 0) throw new ArgumentOutOfRangeException(nameof(damage));

        Value -= damage;

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

    public void Reset() => Value = _maxValue;
}