using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager gameManager;

    public AudioClip checkpointSound; // Sound played when the power-up is collected
    public GameObject checkPointEffect; // Visual effect when the power-up is collected
    public Transform respawnPoint;
    public GameObject checkPointOn;
    public GameObject checkPointOff;
    Collider coll;

    void Start()
    {
        gameManager = GameManager.gameManager;
        coll = GetComponent<Collider>();
        checkPointOn.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(checkPointEffect, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(checkpointSound, transform.position);
            checkPointOff.SetActive(false);
            checkPointOn.SetActive(true);
            gameManager.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;

        }
    }
}
