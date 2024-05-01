using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public float playerRange = 1f;
    public int attackDamage = 10;
    public float attackRange = 3f;
    public float damageInterval = 1f; // Interval between damage ticks
    public int damagePerTick = 5; // Damage per tick

    private Transform player;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isChasing)
        {
            // Chase the player
            ChasePlayer();
        }
        else
        {
            // Patrol between patrol points
            Patrol();
        }
    }

    void Patrol()
    {
        // Move towards the current patrol point
        Transform target = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Check if reached the current patrol point
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Move to the next patrol point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void ChasePlayer()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Rotate to face the player
            transform.LookAt(player);

            // Check if the player is within attack range
            if (Vector3.Distance(transform.position, player.position) <= playerRange)
            {
                // Attack the player
                Attack();
            }
        }
        else
        {
            // If the player is not within detection range, stop chasing
            isChasing = false;
        }
    }


    void Attack()
    {
        // Check if the player is within player range
        if (Vector3.Distance(transform.position, player.position) <= playerRange)
        {
            if (!isAttacking)
            {
                // Start applying damage over time
                StartCoroutine(DealDamageOverTime());
            }
        }
        else
        {
            // Stop applying damage over time if player is not within player range
            StopCoroutine(DealDamageOverTime());
            isAttacking = false;
        }
    }

    IEnumerator DealDamageOverTime()
    {
        isAttacking = true;
        while (Vector3.Distance(transform.position, player.position) <= playerRange)
        {
            // Deal damage to the player every tick
            player.GetComponent<PlayerBehaviour>().TakeDamage(damagePerTick);

            // Wait for the next damage tick
            yield return new WaitForSeconds(damageInterval);
        }
        isAttacking = false;
    }

    // Add logic to switch between patrolling and chasing modes based on player detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the attack sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw the player range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerRange);
    }
}
