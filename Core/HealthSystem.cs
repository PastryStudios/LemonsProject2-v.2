using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDeath;
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnDamaged?.Invoke(this, EventArgs.Empty);
        if (IsDead())
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    public int GetHealthAmount()
    {
        return currentHealth;
    }
    public void SetHealthAmountMax(int maxHealth, bool updateHealthAmount)
    {
        this.maxHealth = maxHealth;
        if (updateHealthAmount)
        {
            currentHealth = maxHealth;
        }
    }
    public float CurrentHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }
    public bool IsFullHealth()
    {
        return currentHealth == maxHealth;
    }
}
