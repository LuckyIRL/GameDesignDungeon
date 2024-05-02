using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public ParticleSystem deathEffect;

    private void Start()
    {
        currentHealth = maxHealth;
        deathEffect = GetComponent<ParticleSystem>();
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
        // Add any death behavior here, such as playing an animation or spawning effects
        deathEffect.Play();
        Destroy(gameObject);
    }

    
}
