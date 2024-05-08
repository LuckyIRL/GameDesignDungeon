using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone7 : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public float respawnTime = 1.0f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.Respawn(respawnTime);
        }
    }
}
