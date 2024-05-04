using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayerTest : MonoBehaviour
{
    PlayerBehaviour playerBehaviour;

    private void Start()
    {
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerBehaviour.TakeDamage(10);
        }
    }
}
