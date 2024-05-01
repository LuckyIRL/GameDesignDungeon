using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(100, 100);

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject); // Don't destroy the GameManager when loading new scenes
        }
    }

    public void RestartLevel()
    {
        // Reset player's health
        _playerHealth.Health = _playerHealth.MaxHealth;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RespawnPlayerAtLastCheckpoint()
    {
        // Get the respawn position from the last checkpoint
        Vector3 respawnPosition = Checkpoint.GetRespawnPosition();

        // Move player to respawn position
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = respawnPosition;
        }
        else
        {
            Debug.LogWarning("Player not found for respawn.");
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    // Game over flag
    public bool isGameOver = false;

}
