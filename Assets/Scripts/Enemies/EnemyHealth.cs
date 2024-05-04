using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public GameObject deathEffect;
    public float deathEffectDuration = 1f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        // Add any death behavior here, such as playing an animation or spawning effects
        deathEffect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffect, deathEffectDuration);
        
    }

    
}
