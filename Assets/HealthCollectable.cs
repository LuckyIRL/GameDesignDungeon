using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public int healthAmount = 10;
    public GameObject collectEffect;
    public AudioClip collectSound;
    PlayerBehaviour playerBehaviour;

    private void Start()
    {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the collect effect
            Instantiate(collectEffect, transform.position, Quaternion.identity);
            // Play the collect sound
            AudioClip clip = collectSound;
            AudioSource.PlayClipAtPoint(clip, transform.position);
            // Increase the player's health
            playerBehaviour.HealPlayer(healthAmount);
            // Destroy the collectable object
            Destroy(gameObject);
        }
    }

}
