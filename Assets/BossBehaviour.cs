using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    public UnitHealth health;
    public Animator animator;
    public Transform player;
    public ArrowBehavior arrow;

    public void Start()
    {
        health = new UnitHealth(100, 100);
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        arrow = GetComponent<ArrowBehavior>();

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
