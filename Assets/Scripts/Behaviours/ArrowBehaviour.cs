using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float timeToDestroy = 5f;
    private RaycastHit hit;

    [SerializeField] public Transform vfxHitGreen;
    [SerializeField] public Transform vfxHitRed;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            transform.LookAt(hit.point);
        }
    }

    private void Start()
    {
        float speed = 100f;
        _rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.GetComponent<AimTarget>() != null)
        {
            // Hit target instansiate green vfx
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            
        }
        else
        {
            // Hit something else instansiate red vfx
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        // wait for timeToDestroy before destroying the arrow
        Destroy(gameObject, timeToDestroy);
        

    }



}
