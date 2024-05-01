using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;

    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerBehaviour.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
