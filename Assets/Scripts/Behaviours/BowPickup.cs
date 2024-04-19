using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPickup : MonoBehaviour
{
    // On trigger enter method to check if the player collides with the bow pickup and if they do, Activate the bow prefab on the player and destroy the bow pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ActivateBow();
            Destroy(gameObject);
        }
    }

}
