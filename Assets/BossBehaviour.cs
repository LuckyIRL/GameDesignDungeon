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
    private ArrowBehavior arrow;
    public NavMeshAgent navMeshAgent;
    public float meleeRange = 2f;
    public float rangedRange = 10f;
    public float jumpRange = 5f;
    public AttackType currentAttack;

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

    }

    public void Attack()
    {
        // Calculate the distance between the boss and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within melee range
        if (distanceToPlayer <= meleeRange)
        {
            // If so, trigger a melee attack
            MeleeAttack();
        }
        // If the player is not in melee range, check if the player is within ranged attack range
        else if (distanceToPlayer <= rangedRange)
        {
            // If so, trigger a ranged attack
            RangedAttack();
        }
        else
        {
            // If the player is out of both melee and ranged attack ranges, the boss can decide to move closer to the player or perform other actions.
        }
    }


    private void MeleeAttack()
    {
        // Trigger melee attack animation
        animator.SetTrigger("MeleeAttack");
        // Implement any additional logic related to melee attack
    }

    private void RangedAttack()
    {
        // Trigger ranged attack animation
        animator.SetTrigger("RangedAttack");
        // Implement any additional logic related to ranged attack
    }

    private void JumpAttack()
    {
        // Trigger jump attack animation
        animator.SetTrigger("JumpAttack");
        // Implement any additional logic related to jump attack
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
