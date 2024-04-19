using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    // Variables and Methods to add force to the arrow's direction and speed and target
    public float _speed = 10.0f;
    private Vector3 forwardDirection;

    // Public property for the target
    public Vector3 Target { get; set; }

    private void Awake()
    {
        forwardDirection = transform.forward;
    }

    private void Update()
    {
        // Calculate the direction towards the target
        Vector3 direction = (Target - transform.position).normalized;

        // Move the arrow towards the target
        transform.position += direction * _speed * Time.deltaTime;

        // Check if the arrow is close enough to the target
        if (Vector3.Distance(transform.position, Target) < 0.5f)
        {
            // If the arrow reaches its target, you can destroy it or handle it as needed
            Destroy(gameObject);
        }
    }

}
