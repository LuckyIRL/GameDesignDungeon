using UnityEngine;
using UnityEngine.AI;

public class BossBehavior : MonoBehaviour
{
    public enum AttackType
    {
        Melee,
        Ranged,
        Jump
        // Add more attack types as needed
    }


    public UnitHealth health;
    public Animator animator;
    public Transform player;
    public PlayerBehaviour playerBehaviour;
    private ArrowBehavior arrow;
    public NavMeshAgent navMeshAgent;
    public float meleeRange = 2f;
    public float rangedRange = 10f;
    public float jumpRange = 5f;
    public AttackType currentAttack;
    public int meleeDamage = 10;
    public int rangedDamage = 5;
    public int jumpDamage = 15;
    // boss projectile
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 2f;
    private float timeSinceLastAttack = 0f;
    public float attackCooldown = 2f;



    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= meleeRange)
        {
            currentAttack = AttackType.Melee;
        }
        else if (distanceToPlayer <= jumpRange)
        {
            currentAttack = AttackType.Jump;
        }
        else
        {
            currentAttack = AttackType.Ranged;
        }

        PerformAttack();
    }

    void PerformAttack()
    {
        switch (currentAttack)
        {
            case AttackType.Melee:
                // Perform melee attack
                animator.SetTrigger("MeleeAttack");
                break;
            case AttackType.Jump:
                // Perform jump attack
                animator.SetTrigger("JumpAttack");
                break;
            case AttackType.Ranged:
                // Perform ranged attack
                animator.SetTrigger("RangedAttack");
                break;
        }
    }

    // method to handle the boss damaging the player
    public void DamagePlayer()
    {
        switch (currentAttack)
        {
            case AttackType.Melee:
                playerBehaviour.TakeDamage(meleeDamage);
                break;
            case AttackType.Jump:
                playerBehaviour.TakeDamage(jumpDamage);
                break;
            case AttackType.Ranged:
                playerBehaviour.TakeDamage(rangedDamage);
                break;
        }
    }





    // Draw gizmos to visualize the attack ranges
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, rangedRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, jumpRange);
    }


    public void Start()
    {
        health = new UnitHealth(100, 100);
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        arrow = GetComponent<ArrowBehavior>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();


    }

    public void Attack()
    {
        // Check the distance between the boss and the player
        float distance = Vector3.Distance(player.position, transform.position);

        // Check which attack to use based on the distance between the player and the boss

        if (distance <= 2.0f)
        {
            MeleeAttack();
        }
        else if (distance <= 10.0f)
        {
            RangedAttack();
        }
        else
        {
            JumpAttack();
        }
    }

    private void MeleeAttack()
    {
        // Trigger melee attack animation
        animator.SetTrigger("MeleeAttack");
        // Damage the player
        playerBehaviour.TakeDamage(meleeDamage);
    }

    private void RangedAttack()
    {
        // Trigger ranged attack animation
        animator.SetTrigger("RangedAttack");
        // Instantiate the projectile
        GameObject projectileInstance = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        // Get the rigidbody of the projectile
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        // Add force to the projectile
        rb.AddForce(projectileSpawnPoint.forward * projectileSpeed, ForceMode.Impulse);
        // Destroy the projectile after a certain amount of time
        Destroy(projectileInstance, projectileLifetime);


    }

    private void JumpAttack()
    {
        // Trigger jump attack animation
        animator.SetTrigger("JumpAttack");
        // Damage the player
        playerBehaviour.TakeDamage(jumpDamage);
    }

    // set the triggers to play the Die animation and the Damage animation
    public void TakeDamage(int arrowDamage)
    {
        
        if (health.Health > 0)
        {
            health.Health -= arrowDamage;
            animator.SetTrigger("Damage");
            Debug.Log("Boss Health: " + health.Health);
            GetComponent<Collider>().enabled = false;
            AudioManager.instance.PlaySFX("Hit");
        }
        else
        {
            animator.SetTrigger("Die");
            AudioManager.instance.PlaySFX("Die");
            Debug.Log("Boss is dead");
        }
    }
}
