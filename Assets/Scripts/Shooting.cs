using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;

public class Shooting : MonoBehaviour
{
    private Animator _animator;
    private StarterAssetsInputs _input;
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    private bool isAiming;
    private bool isShooting;
    private GameManager _gameManager;
    public ThirdPersonController thirdPersonController;
    public GameObject arrowPrefab;
    public Transform arrowSpawnPoint;
    private InputAction _shootAction;
    private InputAction _aimAction;
    [SerializeField] private Canvas aimCanvas;
    [SerializeField] private Canvas followCanvas;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<StarterAssetsInputs>(); // Initialize the input actions
        _gameManager = FindObjectOfType<GameManager>();
        aimCamera = GameObject.Find("AimCamera").GetComponent<CinemachineVirtualCamera>();
        followCamera = GameObject.Find("FollowCamera").GetComponent<CinemachineVirtualCamera>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        aimCanvas = GameObject.Find("AimCanvas").GetComponent<Canvas>();
        followCanvas = GameObject.Find("FollowCanvas").GetComponent<Canvas>();
    }

    // Switch between the follow camera and the aim camera
    private void Update()
    {
        if (_input.isAiming)
        {
            aimCamera.Priority = 20;
            followCamera.Priority = 10;
            _input.isAiming = true;
            aimCanvas.enabled = true;
            followCanvas.enabled = false;
            Debug.Log("Aiming");
        }
        else
        {
            aimCamera.Priority = 10;
            followCamera.Priority = 20;
            _input.isAiming = false;
            aimCanvas.enabled = false;
            followCanvas.enabled = true;
        }
        if (_input.isShooting)
        {
            Debug.Log("Shooting");
            Shoot();
        }
    }



    // Find the center of the screen and shoot an arrow in that direction
    private void Shoot()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
            arrow.GetComponent<Rigidbody>().AddForce(arrowSpawnPoint.forward * 1000);
            
            Debug.Log("Hit: " + hit.transform.name);
        }
    }
}
