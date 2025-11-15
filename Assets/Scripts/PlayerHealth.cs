using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
        EventManager.PlayerDamaged(currentHealth);
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        
        EventManager.PlayerDamaged(currentHealth);
        
        
        if (currentHealth <= 0)
            Die();
    }
    
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(maxHealth, currentHealth);
        EventManager.PlayerDamaged(currentHealth);
    }
    
    void Die()
    {
        Debug.Log("Player died!");
        EventManager.PlaySound("PlayerDeath");
    }
    
    public int GetHealth()
    {
        return currentHealth;
    }
}