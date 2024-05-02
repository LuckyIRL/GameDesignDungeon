using UnityEngine;

public class ArrowDispenser : MonoBehaviour
{
    public GameObject arrowPickupPrefab; // Reference to the arrow pickup prefab
    public Transform spawnPoint; // Point where the arrow pickup will spawn

    public float cooldownDuration = 1.0f; // Cooldown duration in seconds
    private bool canDispense = true; // Flag to track whether dispenser can dispense arrows

    // Define interaction logic when player enters the dispenser's trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canDispense)
        {
            DispenseArrow();
            StartCooldown();
        }
    }

    // Spawn an arrow pickup at the spawn point
    private void DispenseArrow()
    {
        if (arrowPickupPrefab != null && spawnPoint != null)
        {
            Instantiate(arrowPickupPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogWarning("Arrow pickup prefab or spawn point not assigned in the dispenser.");
        }
    }

    // Start the cooldown period
    private void StartCooldown()
    {
        canDispense = false;
        Invoke("ResetDispenser", cooldownDuration);
    }

    // Reset the dispenser after cooldown period
    private void ResetDispenser()
    {
        canDispense = true;
    }
}
