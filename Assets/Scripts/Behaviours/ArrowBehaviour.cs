using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private float arrowSpeed = 50f;
    [SerializeField] private float timeToDestroy = 5f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }
    
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<Rigidbody>());
    }
}
