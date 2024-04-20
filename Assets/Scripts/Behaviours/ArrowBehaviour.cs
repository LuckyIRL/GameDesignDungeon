using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private float _initialSpeed = 10.0f; // Initial speed of the arrow
    [SerializeField] private float _acceleration = 0.0f; // Acceleration of the arrow
    [SerializeField] private float _maxSpeed = 30.0f; // Maximum speed of the arrow
    [SerializeField] private float _turnSpeed = 10.0f; // Turning speed of the arrow
    [SerializeField] private float _lifetime = 5.0f; // Lifetime of the arrow

    private Vector3 _velocity; // Current velocity of the arrow
    private float _elapsedTime; // Elapsed time since the arrow was instantiated
    private bool _isDestroyed; // Flag to determine if the arrow has been destroyed

    // Public property for the target
    public Vector3 Target { get; set; }

    // Method to set the initial direction and velocity of the arrow
    private void Start()
    {
        _velocity = transform.forward * _initialSpeed;
        _elapsedTime = 0.0f;
        _isDestroyed = false;
    }

    // Method to update the arrow's movement and behavior
    private void Update()
    {
        if (_isDestroyed)
        {
            return; // Don't update if the arrow has been destroyed
        }

        // Update the elapsed time
        _elapsedTime += Time.deltaTime;

        // Calculate the direction towards the target
        Vector3 direction = (Target - transform.position).normalized;

        // Apply acceleration to the velocity
        _velocity += direction * _acceleration * Time.deltaTime;

        // Limit the speed of the arrow to the maximum speed
        _velocity = Vector3.ClampMagnitude(_velocity, _maxSpeed);

        // Move the arrow based on its velocity
        transform.position += _velocity * Time.deltaTime;

        // Rotate the arrow towards its direction of movement
        if (_velocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        }

        // Check if the arrow has reached its target or exceeded its lifetime
        if (Vector3.Distance(transform.position, Target) < 0.5f || _elapsedTime >= _lifetime)
        {
            DestroyArrow();
        }
    }

    // Method to destroy the arrow
    private void DestroyArrow()
    {
        _isDestroyed = true;
        Destroy(gameObject);
    }

    public void SetDrawStrength(float drawStrength)
    {
        // Adjust arrow velocity based on draw strength
        _velocity = transform.forward * (_initialSpeed * drawStrength);
    }

}
