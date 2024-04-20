using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player is jumping (you may need to adjust the condition based on your player's jumping mechanics)
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody.velocity.y > 0.0f)
            {
                // Destroy the enemy
                Destroy(gameObject);
            }
        }
    }
}
