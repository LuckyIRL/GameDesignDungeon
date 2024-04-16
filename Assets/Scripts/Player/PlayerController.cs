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
    private CinemachineVirtualCamera _aimCamera;
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

    // Shoot Method to spawn the arrow to shoot at the center of the screen where the player is aiming using Raycasttohit

    public void Shoot(InputAction.CallbackContext context)
    {
        if (!context.started || !_isArrowReady) return;

        if (_movement.isSprinting)
        {
            _switchCam.CancelAim();
        }

        if (_isArrowReady)
        {
            _isArrowReady = false;
            StartCoroutine(ResetArrow());
            var arrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, Quaternion.identity, arrowParent);
            arrowBehaviour = arrow.GetComponent<ArrowBehaviour>();
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, arrowHitMissDistance))
            {
                arrowBehaviour.target = hit.point;
            }
            else
            {
                arrowBehaviour.target = ray.GetPoint(arrowHitMissDistance);
            }
        }
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
