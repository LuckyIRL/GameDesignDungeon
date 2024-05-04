using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(100, 100);
    private Vector3 checkpointPos;
    PlayerBehaviour playerBehaviour;

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        checkpointPos = GameObject.FindGameObjectWithTag("Player").transform.position; // Find the player's starting position
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    public void UpdateCheckpoint(Vector3 pos)
    {
        // Update the checkpoint position
        checkpointPos = pos;
    }

    public IEnumerator Respawn(float delay)
    {
        // Wait for the delay time
        yield return new WaitForSeconds(delay);
        // Respawn the player at the checkpoint position
        GameObject.FindGameObjectWithTag("Player").transform.position = checkpointPos;
        // Reset the player's health
        playerBehaviour.health.Health = playerBehaviour.health.MaxHealth;
        playerBehaviour._healthbar.SetHealth(playerBehaviour.health.Health);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator GameOver(float delay)
    {
        // Wait for the delay time
        yield return new WaitForSeconds(delay);
        // Load the Game Over scene
        SceneManager.LoadScene("EndScene");
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }



}
