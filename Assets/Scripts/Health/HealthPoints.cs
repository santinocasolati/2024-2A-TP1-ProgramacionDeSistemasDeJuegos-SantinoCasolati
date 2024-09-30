using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    protected float currentHealth;

    public event Action OnDeath = delegate { };
    public event Action OnSpawn = delegate { };

    protected virtual void Start()
    {
        ResetHealth();
    }

    private void OnEnable()
    {
        OnSpawn?.Invoke();
    }

    public virtual void Damage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        OnDeath?.Invoke();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
