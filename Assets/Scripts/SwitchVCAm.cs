using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchVCAm : MonoBehaviour
{
    [SerializeField] private StarterAssetsInputs _input;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera followCamera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera aimCamera;

    private InputAction _aimAction;

    private void Awake()
    {
        aimCamera = GameObject.Find("PlayerAimCamera").GetComponent<Cinemachine.CinemachineVirtualCamera>();
        // Initialize the input actions Aiming and Shooting
        _input = GetComponent<StarterAssetsInputs>();
        _aimAction = GetComponent<InputAction>();
    }

    private void OnEnable()
    {
        _aimAction.performed += _ => StartAim();
        _aimAction.canceled += _ => StopAim();
    }

    private void OnDisable()
    {
        _aimAction.performed -= _ => StartAim();
        _aimAction.canceled -= _ => StopAim();
    }

    private void StartAim()
    {
        aimCamera.Priority = 20;
        followCamera.Priority = 10;
        _input.isAiming = true;
        Debug.Log("Aiming");
    }

    private void StopAim()
    {
        aimCamera.Priority = 10;
        followCamera.Priority = 20;
        _input.isAiming = false;
    }

}
