using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems itemType;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Add the key to the inventory and update the UI
            InventoryManager.instance.AddItem(itemType);
            InventoryManager.instance.UpdateUI();
            // Destroy the key
            Destroy(gameObject);
        }
    }
}
