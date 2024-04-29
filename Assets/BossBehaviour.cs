using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public UnitHealth health;
    public Animator animator;
    public Transform player;

    public void Start()
    {
        health = new UnitHealth(100, 100);
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // set the triggers to play the Die animation and the Damage animation
    public void TakeDamage(int arrowDamage)
    {
        if (health.Health > 0)
        {
            health.Health -= 10;
            animator.SetTrigger("Damage");
            Debug.Log("Boss Health: " + health.Health);
        }
        else
        {
            animator.SetTrigger("Die");
            Debug.Log("Boss is dead");
        }
    }
}
