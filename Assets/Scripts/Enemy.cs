using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            // Reduce enemy health when hit by an arrow
            TakeDamage(20); // Adjust the amount of damage as needed
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform death actions (e.g., play death animation, spawn particle effects, etc.)
        Destroy(gameObject);
    }
}
