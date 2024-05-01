using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform startPoint; // The starting position of the elevator
    public Transform endPoint;   // The end position of the elevator
    public float speed = 2.0f;   // Speed of the elevator

    private Transform playerAttachedToElevator; // Reference to the player attached to the elevator
    public bool playerOnElevator; // Flag to check if the player is on the elevator in the inspector

    // script to lerp the elevator between the start and end points with the player attached to it at a constant speed only once the player enters the box collider trigger, with comments to explain the code
    private void Update()
    {
        // Check if the player is on the elevator
        if (playerOnElevator)
        {
            // Move the elevator towards the end point
            

            // Check if the player is attached to the elevator
            if (playerAttachedToElevator != null)
            {
                // Move the player along with the elevator
                transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
            }
        }
    }

    // Method to handle the player entering the elevator trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger collider
        if (other.CompareTag("Player"))
        {
            // Set the player on the elevator flag to true
            playerOnElevator = true;
            Debug.Log("Player on elevator");

            // Attach the player to the elevator
            playerAttachedToElevator = other.transform;
        }
    }

    // Method to handle the player exiting the elevator trigger collider
    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the trigger collider
        if (other.CompareTag("Player"))
        {
            // Set the player on the elevator flag to false
            playerOnElevator = false;

            // Detach the player from the elevator
            playerAttachedToElevator = null;
        }
    }

    // Method to draw the trigger collider in the scene view for easier visualization
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    // Method to draw the start and end points of the elevator in the scene view for easier visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint.position, 0.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(endPoint.position, 0.5f);
    }
}
