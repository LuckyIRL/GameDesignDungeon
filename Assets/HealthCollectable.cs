using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    PlayerBehaviour player;
    public int healthAmount = 10;
    public GameObject collectEffect;
    public AudioClip collectSound;

    private void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collectEffect = Instantiate(collectEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

            player.Heal(healthAmount);
            Destroy(gameObject);
        }
    }
}
