using System.Collections;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    // Define a respawn delay
    public float respawnDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding GameObject is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Access the GameManager instance and start the respawn coroutine
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.Respawn(respawnDelay));
        }
    }
}
