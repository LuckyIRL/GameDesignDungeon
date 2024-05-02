using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private float timeToDestroy = 5f;
    private RaycastHit hit;

    [SerializeField] private int arrowDamage; // Damage inflicted by the arrow

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

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
        BossBehavior boss = other.GetComponent<BossBehavior>();
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        if (boss != null)
        {
            boss.TakeDamage(arrowDamage);
            Destroy(gameObject);
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else if (enemy != null)
        {
            enemy.TakeDamage(arrowDamage);
            Destroy(gameObject);
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
