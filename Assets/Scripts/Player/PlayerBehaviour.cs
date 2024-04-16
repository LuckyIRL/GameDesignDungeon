using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Healthbar _healthbar;
    public float arrowsCollected;

    void Start()
    {
        // Set the initial health of the player in the health bar
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }

    // Method to take damage when hit by the boss
    public void TakeDamage(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);

        // Check if player's health reaches zero
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            // Call the RestartLevel method from GameManager if player's health is zero
            GameManager.gameManager.RestartLevel();
        }
    }

    // Method to heal the player when collecting a health pickup
    public void Heal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
    }
    public void UseArrow()
    {
        arrowsCollected -= 1;
    }
}
