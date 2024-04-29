//using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPickup : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems itemType = InventoryManager.AllItems.Bow;
    // Reference to the bow prefab
    public GameObject bowPrefab;
    // On trigger enter method to check if the player collides with the bow pickup and if they do, Activate the bow prefab on the player and destroy the bow pickup

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(itemType);
            InventoryManager.instance.hasBow = true;
            InventoryManager.instance.UpdateUI();
            bowPrefab.SetActive(true);
            Destroy(gameObject);
        }
    }

}
