using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint lastCheckpoint;
    public AudioClip  checkpointSound; // Sound played when the power-up is collected
    public GameObject checkPointEffect; // Visual effect when the power-up is collected

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Set this checkpoint as the last checkpoint when player reaches it
            lastCheckpoint = this;
            Instantiate(checkPointEffect, transform.position, Quaternion.identity);
            if (checkpointSound != null)
            {
                AudioSource.PlayClipAtPoint(checkpointSound, transform.position);
            }

            // Disable the collider to prevent multiple triggers
            GetComponent<Collider>().enabled = false;
        }
    }

    public static Vector3 GetRespawnPosition()
    {
        // Return the position of the last checkpoint reached
        if (lastCheckpoint != null)
        {
            return lastCheckpoint.transform.position;
        }
        else
        {
            // If no checkpoint reached, respawn at origin
            return Vector3.zero;
        }
    }

    public static void ResetCheckpoint()
    {
        // Reset the last checkpoint to null
        lastCheckpoint = null;
    }
    public static void Update()
    {
        if (lastCheckpoint != null)
        {
               lastCheckpoint.GetComponent<Collider>().enabled = true;
        }
    }

    public static void Reset()
    {
        lastCheckpoint = null;
    }

    public static void SetCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    public static Checkpoint GetCheckpoint()
    {
        return lastCheckpoint;
    }

    public static void DisableCheckpoint()
    {
        lastCheckpoint.GetComponent<Collider>().enabled = false;
    }

    public static void EnableCheckpoint()
    {
        lastCheckpoint.GetComponent<Collider>().enabled = true;
    }

}
