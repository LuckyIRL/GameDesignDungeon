using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCam : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;

    private CinemachineVirtualCamera _aimCamera;
    private InputAction _aimAction;
    [SerializeField] public int priorityBoost = 10;

    [SerializeField] Canvas _crosshair;
    [SerializeField] Canvas _mainCanvas;

    private void Awake()
    {
        _aimCamera = GetComponent<CinemachineVirtualCamera>();
        _aimAction = _playerInput.actions["Aim"];
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
        _aimCamera.Priority += priorityBoost;
        _crosshair.enabled = true;
        _mainCanvas.enabled = false;
    }

    public void CancelAim()
    {
        _aimCamera.Priority -= priorityBoost;
        _crosshair.enabled = false;
        _mainCanvas.enabled = true;
    }

}
