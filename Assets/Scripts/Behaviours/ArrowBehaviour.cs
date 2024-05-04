using UnityEngine;

public class ArrowBehavior : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private RaycastHit hit;

    [SerializeField] private int arrowDamage; // Damage inflicted by the arrow

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    [SerializeField] public BossBehavior boss;
    [SerializeField] public EnemyHealth enemy;

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
            transform.parent = boss.transform;
            boss.TakeDamage(arrowDamage);
            Destroy(gameObject);
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            Debug.Log("Hit boss");
        }
        else if (enemy != null)
        {
            transform.parent = enemy.transform;
            enemy.TakeDamage(arrowDamage);
            Destroy(gameObject);
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            Debug.Log("Hit enemy");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
