using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Healthbar _healthbar; // Reference to the health bar script
    public int arrowsCollected = 0;

    void Start()
    {
    }

    // Method to take damage when hit by the boss
    public void TakeDamage(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        if (_healthbar != null)
        {
            _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
        }
        else
        {
            Debug.LogWarning("Health bar reference is not set in PlayerBehaviour.");
        }

        // Check if player's health reaches zero
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.Respawn(.5f));
        }
    }

    // Method to heal the player when collecting a health pickup
    public void Heal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        if (_healthbar != null)
        {
            _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
        }
        else
        {
            Debug.LogWarning("Health bar reference is not set in PlayerBehaviour.");
        }
    }

    public void UseArrow()
    {
        arrowsCollected -= 1;
    }
}
