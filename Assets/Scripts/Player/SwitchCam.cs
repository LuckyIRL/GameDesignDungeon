using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchCam : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;

    private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    private InputAction _aimAction;
    [SerializeField] public int priorityBoost = 10;
    [SerializeField] public static bool IsAiming = false;

    [SerializeField] Canvas _crosshair;
    [SerializeField] Canvas _mainCanvas;

    private int _originalPriority;

    // Property to get the current camera based on the aim status
    public CinemachineVirtualCamera CurrentCamera
    {
        get { return SwitchCam.IsAiming ? _aimCamera : _mainCamera; }
    }


    private void Awake()
    {
        _aimCamera = GetComponent<CinemachineVirtualCamera>();
        _aimAction = _playerInput.actions["Aim"];
        _originalPriority = _aimCamera.Priority;
    }

    private void OnEnable()
    {
        _aimAction.performed += _ => StartAim();
        _aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        _aimAction.performed -= _ => StartAim();
        _aimAction.canceled -= _ => CancelAim();
    }

    public void StartAim()
    {
        _aimCamera.Priority = _originalPriority + priorityBoost;
        _crosshair.enabled = true;
        _mainCanvas.enabled = false;
    }

    public void CancelAim()
    {
        _aimCamera.Priority = _originalPriority;
        _crosshair.enabled = false;
        _mainCanvas.enabled = true;
    }
}
