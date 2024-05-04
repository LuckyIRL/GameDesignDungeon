using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    public UnitHealth health;
    public int arrowsCollected = 0;
    public int maxArrows = 100;
    public Healthbar _healthbar; // Add the _healthbar field
    public GameManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health = new UnitHealth(100, 100);
        // Assign the reference to the Healthbar component
        _healthbar = FindObjectOfType<Healthbar>();
        // Set the initial value of the health bar
        if (_healthbar != null)
        {
            _healthbar.SetMaxHealth(health.MaxHealth);
            _healthbar.SetHealth(health.Health);
        }
    }

    private void LateUpdate()
    {
        if (health.Health <= 0)
        {
            StartCoroutine(gameManager.Respawn(0.1f));
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health.Health -= damageAmount;
        if (_healthbar != null)
        {
            _healthbar.SetHealth(health.Health);
        }
    }

    public void HealPlayer(int healAmount)
    {
        health.Health += healAmount;
        if (health.Health > health.MaxHealth)
        {
            health.Health = health.MaxHealth;
        }
        if (_healthbar != null)
        {
            _healthbar.SetHealth(health.Health);
        }
    }

    public void CollectArrows(int arrowAmount)
    {
        arrowsCollected += arrowAmount;
        if (arrowsCollected > maxArrows)
        {
            arrowsCollected = maxArrows;
        }
    }

    public void UseArrow()
    {
        if (arrowsCollected > 0)
        {
            arrowsCollected--;
        }
    }
}
