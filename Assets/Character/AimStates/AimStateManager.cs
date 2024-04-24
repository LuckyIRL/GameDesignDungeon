using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;
    public AimHipFireState Hip = new AimHipFireState();
    public AimState Aim = new AimState();

    [SerializeField] float mouseSensitivity = 100.0f;
    private float xAxis, yAxis;
    [SerializeField] Transform camFollowPos;
    public InputAction aimX, aimY;

    [HideInInspector] public Animator Anim;
    [HideInInspector] public CinemachineVirtualCamera aimCam;
    public float adsFOV = 40;
    [HideInInspector] public float hipFOV;
    [HideInInspector] public float currentFOV;
    public float adsSmoothSpeed = 10f;

    [SerializeField] private Transform aimPos;
    [SerializeField] private float aimSmoothSpeed = 20;
    [SerializeField] private LayerMask aimMask;


    // Start is called before the first frame update
    void Start()
    {
        aimCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFOV = aimCam.m_Lens.FieldOfView;
        Anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        yAxis = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        yAxis = Mathf.Clamp(yAxis, -80, 80);

        aimCam.m_Lens.FieldOfView = Mathf.Lerp(aimCam.m_Lens.FieldOfView, currentFOV, adsSmoothSpeed * Time.deltaTime);

        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCentre);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);


        currentState.UpdateState(this);
    }

    private void LateUpdate()
    {
        camFollowPos.localEulerAngles = new Vector3(yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.localEulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
}
