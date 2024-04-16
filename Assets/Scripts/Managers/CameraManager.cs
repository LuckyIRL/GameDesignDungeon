using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private float _distanceToPlayer;
    private Vector2 _input;

    [SerializeField] private MouseSentitivity _mouseSensitivity;
    [SerializeField] private CameraAngle _cameraAngle;
    private CameraRotation _cameraRotation;

    private void Awake()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _target.position);
    }

    public void Look(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        _cameraRotation.Yaw += _input.x * _mouseSensitivity.horizontal * BoolToInt(_mouseSensitivity.InvertHorizontal) * Time.deltaTime;
        _cameraRotation.Pitch += _input.y * _mouseSensitivity.vertical * BoolToInt(_mouseSensitivity.InvertVertical) * Time.deltaTime;
        _cameraRotation.Pitch = Mathf.Clamp(_cameraRotation.Pitch, _cameraAngle.min, _cameraAngle.max);
    }

    private void LateUpdate()
    {
        transform.eulerAngles = new Vector3(_cameraRotation.Pitch, _cameraRotation.Yaw, 0.0f);
        transform.position = _target.position - transform.forward * _distanceToPlayer;
    }

    private static int BoolToInt(bool b) => b ? 1 : -1;

}

[Serializable]
public struct MouseSentitivity
{
    public float horizontal;
    public float vertical;
    public bool InvertHorizontal;
    public bool InvertVertical;
}

public struct CameraRotation
{
    public float Pitch;
    public float Yaw;
}

[Serializable]
public struct CameraAngle
{
    public float min;
    public float max;
}
