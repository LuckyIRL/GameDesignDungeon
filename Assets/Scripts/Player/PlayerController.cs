using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction;


    // Movement variables
    //[SerializeField] private float _speed = 5.0f;
    [SerializeField] private Movement _movement;
    [SerializeField] private float rotationSpeed = 500f;

    // Camera variables
    private CinemachineVirtualCamera _mainCamera;
    [SerializeField] SwitchCam _switchCam;


    // Gravity variables
    private float _gravity = -9.81f;
    [SerializeField] private float _gravityMultiplier = 3.0f;
    private float _velocity;
    // Jump variables
    [SerializeField] private float _jumpPower = 3.0f;
    private int _numberOfJumps;
    [SerializeField] private int _maxNumberOfJumps = 2;
    // Arrow variables
    private bool _isArrowReady = true;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _arrowSpawnPoint;
    [SerializeField] private float _arrowCooldown = 1.0f;
    [SerializeField] private Transform arrowParent;
    [SerializeField] private float arrowHitMissDistance = 25;
    private Transform cameraTransform;
    private ArrowBehaviour arrowBehaviour;
    public bool hasBow = false;
    [SerializeField] private Transform _bowParent;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _switchCam = GetComponent<SwitchCam>();
        _mainCamera = GameObject.Find("MainCamera").GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        ApplyRotation();
        ApplyGravity();
        ApplyMovement();
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && _velocity < 0.0f)
        {
            _velocity = -0.1f;
        }
        else
        {
            _velocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }

        _direction.y = _velocity;
    }

    private void ApplyRotation()
    {
        if (_input.magnitude == 0.0f) return;

        _direction = Quaternion.Euler(0.0f, _mainCamera.transform.eulerAngles.y, 0.0f) * new Vector3(_input.x, 0.0f, _input.y);
        var targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void ApplyMovement()
    {
        var targetSpeed = _movement.isSprinting ? _movement._speed * _movement._multiplier : _movement._speed;
        _movement._currentSpeed = Mathf.MoveTowards(_movement._currentSpeed, targetSpeed, _movement._acceleration * Time.deltaTime);

        _characterController.Move(_direction * _movement._currentSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>().normalized;
        _direction = new Vector3(_input.x, y: 0.0f, z: _input.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded() && _numberOfJumps >= _maxNumberOfJumps) return;
        if (_numberOfJumps == 0) StartCoroutine(WaitForLanding());

        _numberOfJumps++;
        _velocity = _jumpPower;
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        _movement.isSprinting = context.started || context.performed;
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _switchCam.StartAim();
        }
        else if (context.canceled)
        {
            _switchCam.CancelAim();
        }
    }


    // When called, checks if the player has a Bow and Arrows. Player can not shoot with < 1 arrow.
    // If both conditions are met, instantiates an Arrow prefab and fires it where the player is aiming using a Raycastfrom the camera view
    // This will work in both camera views, main camera and aim camera.
    // using a Raycast from the player's camera and show it with a line renderer.
    // If the Raycast hits something, the arrow aims at the hit point.
    // If the Raycast doesn't hit anything, the arrow aims at a point along the ray at a maximum distance.
    // This method is triggered by player input and is used to shoot arrows from the player's bow.
    // The method also updates the UI to reflect the number of arrows the player has left.

    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot method called."); // Add this line for debugging

        // When called, checks if the player has a Bow and Arrows. Player can not shoot with < 1 arrow.
        if (!context.started || !_isArrowReady || !hasBow || InventoryManager.instance.numberOfArrows < 1)
        {
            Debug.Log("Cannot shoot: Conditions not met."); // Add this line for debugging
            return;
        }

        // Set the arrow as not ready to prevent rapid shooting
        _isArrowReady = false;
        StartCoroutine(ResetArrow());

        // Get the transform of the player's current camera
        Transform cameraTransform = _switchCam.CurrentCamera.transform;

        // Perform a raycast from the center of the screen of the current camera
        Ray ray = cameraTransform.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Instantiate the arrow prefab for shooting
        var arrowObject = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity, arrowParent);
        arrowBehaviour = arrowObject.GetComponent<ArrowBehaviour>();

        // Create a line renderer to visualize the arrow's trajectory
        LineRenderer lineRenderer = arrowObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Perform a raycast to determine the arrow's target point
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, arrowHitMissDistance))
        {
            // If the ray hits something, set the arrow's target to the hit point
            arrowBehaviour.Target = hit.point;
            lineRenderer.SetPosition(0, _arrowSpawnPoint.position);
            lineRenderer.SetPosition(1, hit.point);
            Debug.Log("Raycast hit at: " + hit.point);
        }
        else
        {
            // If the ray doesn't hit anything, set the arrow's target to a point along the ray at a maximum distance
            Vector3 targetPoint = ray.origin + ray.direction * arrowHitMissDistance;
            arrowBehaviour.Target = targetPoint;
            lineRenderer.SetPosition(0, _arrowSpawnPoint.position);
            lineRenderer.SetPosition(1, targetPoint);
            Debug.Log("Raycast did not hit anything. Shooting at maximum distance.");
        }

        // Update the UI
        InventoryManager.instance.numberOfArrows--; // Decrement the number of arrows
        InventoryManager.instance.UpdateUI();
    }



    // Activate PlayerBow Prefab on the player

    public void ActivateBow()
    {
        hasBow = true;
        _bowParent.gameObject.SetActive(true);
    }




    private IEnumerator ResetArrow()
    {
        yield return new WaitForSeconds(_arrowCooldown);
        _isArrowReady = true;
    }


    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => !IsGrounded());
        yield return new WaitUntil(IsGrounded);
        _numberOfJumps = 0;
    }

    private bool IsGrounded() => _characterController.isGrounded;
}

[Serializable]
public struct Movement
{
    public float _speed;
    public float _multiplier;
    public float _acceleration;

    [HideInInspector] public bool isSprinting;
    [HideInInspector] public float _currentSpeed;
}
