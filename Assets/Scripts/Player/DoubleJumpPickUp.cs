using StarterAssets;
using UnityEngine;

public class DoubleJumpPickup : MonoBehaviour
{
    public AudioClip pickupSound; // Sound played when the power-up is collected
    public GameObject pickupEffect; // Visual effect when the power-up is collected
    public ThirdPersonController playerController; // Reference to the player controller

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play pickup sound
            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            }

            // Instantiate pickup effect
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Grant double jump capability to the player
            ThirdPersonController playerController = other.GetComponent<ThirdPersonController>();
            if (playerController != null)
            {
                playerController.ActivateDoubleJumpPowerUp();
                // Update UI sprite to indicate double jump availability
                playerController.SetDoubleJumpIconActive(true);
                // Destroy the power-up object
                Destroy(gameObject);
            }
            // Debug if the player can double jump
            Debug.Log("Can Double Jump: " + playerController.canDoubleJump);


        }
    }
}
