//using StarterAssets;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPickup : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems itemType = InventoryManager.AllItems.Bow;
    // Reference to the bow prefab
    public GameObject bowPrefab;
    public AudioClip pickupSound; // Sound played when the power-up is collected
    public GameObject pickupEffect; // Visual effect when the power-up is collected
    public ThirdPersonController playerController; // Reference to the player controller
    // On trigger enter method to check if the player collides with the bow pickup and if they do, Activate the bow prefab on the player and destroy the bow pickup

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
                InventoryManager.instance.AddItem(itemType);
                InventoryManager.instance.hasBow = true;
                InventoryManager.instance.UpdateUI();
                bowPrefab.SetActive(true);
                playerController.SetBowIconActive(true);

                Destroy(gameObject);
            }
            
        }
    }

}
