using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 10;
    public float attackRange = 2f;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public PlayerBehaviour playerBehaviour;
    private Animator animator;
    public NavMeshAgent navMeshAgent;


    private void Start()
    {
        playerBehaviour = FindObjectOfType<PlayerBehaviour>();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            // Reduce enemy health when hit by an arrow
            TakeDamage(20); // Adjust the amount of damage as needed
            //play hit animation
            animator.SetTrigger("Hit");
            Debug.Log("Enemy hit by arrow!");

        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerBehaviour.transform.position);

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            animator.SetBool("IsAttacking", true);
            playerBehaviour.TakeDamage(damage);
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }




    private void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // play death animation
        animator.SetBool("IsDead", true);
        // Perform death actions (e.g., play death animation, spawn particle effects, etc.)
        Destroy(gameObject);
    }
}
