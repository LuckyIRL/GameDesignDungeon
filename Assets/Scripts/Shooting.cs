using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
using System.Collections;

public class Shooting : MonoBehaviour
{
    private Animator _animator;
    private StarterAssetsInputs _input;
    private InventoryManager _inventory;
    public ThirdPersonController thirdPersonController;

    [Header("Camera Settings")]
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    [SerializeField] private Canvas aimCanvas;
    [SerializeField] private Canvas followCanvas;





    [Header("Arrow Settings")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    public float shootDelay = 0.5f;
    [SerializeField] private bool hasArrows;
    [SerializeField] private bool canShoot = true;


    private InputAction _shootAction;
    private InputAction _aimAction;

    private bool isShooting;

    [Header("Sensitivity")]
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    [Header("Debug")]
    [SerializeField] private Transform debugTransform;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>(); // Initialize the input actions
        _inventory = FindObjectOfType<InventoryManager>();
        aimCamera = GameObject.Find("AimCamera").GetComponent<CinemachineVirtualCamera>();
        followCamera = GameObject.Find("FollowCamera").GetComponent<CinemachineVirtualCamera>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        aimCanvas = GameObject.Find("AimCanvas").GetComponent<Canvas>();
        followCanvas = GameObject.Find("FollowCanvas").GetComponent<Canvas>();
    }

    // Switch between the follow camera and the aim camera
    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        // Cast a ray from the center of the screen to the direction the camera is facing
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = hit.point;
            mouseWorldPosition = hit.point;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            Debug.Log("Hit: " + hit.transform.name);
        }

        // check if the player has arrows in the inventory
        hasArrows = _inventory.HasArrows();

        if (_input.isAiming)
        {
            aimCamera.Priority = 20;
            followCamera.Priority = 10;
            _input.isAiming = true;
            aimCanvas.enabled = true;
            followCanvas.enabled = false;
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            //Debug.Log("Aiming");
        }
        else
        {
            aimCamera.Priority = 10;
            followCamera.Priority = 20;
            _input.isAiming = false;
            aimCanvas.enabled = false;
            followCanvas.enabled = true;
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
        }
        if (_input.isShooting && hasArrows && canShoot)
        {
            Vector3 aimDir = (mouseWorldPosition - arrowSpawnPoint.position).normalized;

            Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            StartCoroutine(ShootDelay());
            // Remove an arrow from the inventory
            _inventory.numberOfArrows--;
            _inventory.UpdateUI();

        }
    }



    // Wait for the delay before shooting again
    private IEnumerator ShootDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootDelay);
        canShoot = true;
    }
}
