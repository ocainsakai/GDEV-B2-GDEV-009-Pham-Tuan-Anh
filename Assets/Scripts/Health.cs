using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public Action onDeath;
    public Action<int> onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        onHealthChanged?.Invoke(currentHealth);
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        onHealthChanged?.Invoke(currentHealth);
    }
    private void Die()
    {
        onDeath?.Invoke();
    }
}
