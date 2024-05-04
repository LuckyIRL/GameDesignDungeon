using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPickup : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems itemType = InventoryManager.AllItems.Arrows;

    //Variables for arrow pickup and inventory
    public GameObject arrow;
    public int arrowAmount = 5;
    public AudioClip collectSound;
    public GameObject collectEffect;


    // Collectable script for the 3D arrow pickup and adding the arrow to the inventory and updating the UI
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectEffect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            InventoryManager.instance.AddItem(itemType);
            InventoryManager.instance.numberOfArrows += arrowAmount;
            InventoryManager.instance.UpdateUI();
            Destroy(gameObject);

        }
    }
}
