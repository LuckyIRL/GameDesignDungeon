using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Method to handle taking damage from arrows
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("Boss Health: " + currentHealth); // Debug log to track health reduction

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die"); // Trigger the death animation
        // Implement death behavior (e.g., play death animation, trigger victory event, etc.)
        Destroy(gameObject);
    }
}
