using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;
    public event EventHandler OnHealthAmountMaxChanged;

    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    private bool isInvincibility = false;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damage)
    {
        if (isInvincibility == true) return;

        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = healthAmountMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public bool IsFullHealthAmount()
    {
        return healthAmount == healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount)
        {
            healthAmount = healthAmountMax;
        }

        OnHealthAmountMaxChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public void OnInvincibility()
    {
        isInvincibility = true;
    }

    public void OffInvincibility()
    {
        isInvincibility = false;
    }
}
