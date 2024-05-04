using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public UnitHealth health;
    public Animator animator;
    public Transform player;
    public PlayerBehaviour playerBehaviour;
    private ArrowBehavior arrow;
    public NavMeshAgent navMeshAgent;
    public int damage = 10;
    public Collider armCollider;
    public AttackState attackState;
    public GameManager gameManager;


    public void Start()
    {
        health = new UnitHealth(100, 100);
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        arrow = GetComponent<ArrowBehavior>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerBehaviour = player.GetComponent<PlayerBehaviour>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // No attack logic in Update method
    }

    // set the triggers to play the Die animation and the Damage animation
    public void TakeDamage(int damageAmount)
    {

        health.Health -= damageAmount;

        if (health.Health <= 0)
        {
            animator.SetTrigger("Die");
            AudioManager.instance.PlaySFX("Die");
            GetComponent<Collider>().enabled = false;
            Die();
            Debug.Log("Boss is dead");
            

        }
        else
        {
            // play the damage animation and sound effect
            animator.SetTrigger("Damage");
            AudioManager.instance.PlaySFX("Hit");
            Debug.Log("Boss Health: " + health.Health);

        }
    }

    // Use the collider on the arm to attack the player when the boss is in the attack animation
    public void AttackPlayer()
    {
        armCollider.enabled = true;
    }

    // on collision with the player, deal damage to the player using the TakeDamage method in the PlayerBehaviour script
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerBehaviour.TakeDamage(damage);
        }
    }

    // Method to be called when the boss dies
    public void Die()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);
        // Load the next scene
        SceneManager.LoadScene("EndScene");
    }
}
