using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private UnitHealth health;
    // Variables for Enemy
    public float speed = 5.0f;
    public float attackSpeed = 1.0f;
    public float attackRange = 1.0f;
    public float attackCooldown = 0.0f;
    public int damage = 1;

    // Setting the health of the enemy
    public void SetHealth(int health, int maxHealth)
    {
        this.health = new UnitHealth(health, maxHealth);
    }

    // Method to follow the player
    public void FollowPlayer(Transform player)
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Method to attack the player
    public void AttackPlayer(Transform player)
    {
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            if (Time.time > attackCooldown)
            {
                attackCooldown = Time.time + attackSpeed;
                PlayerBehaviour playerBehaviour = player.GetComponent<PlayerBehaviour>();
                playerBehaviour.TakeDamage(damage);
            }
        }
    }

    // Method to take damage when hit by the player
    public void TakeDamage(int dmg)
    {
        health.DmgUnit(dmg);
        if (health.Health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
