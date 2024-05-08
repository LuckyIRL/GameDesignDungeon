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

    private void Update()
    {
        checkpointPos = GameObject.FindGameObjectWithTag("Player").transform.position;  
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
        playerBehaviour.transform.position = checkpointPos;
        playerBehaviour.health.Health = playerBehaviour.health.MaxHealth;
        playerBehaviour._healthbar.SetHealth(playerBehaviour.health.Health);
        yield return new WaitForSeconds(delay);
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

    public void LoadNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPreviousScene()
    {
        // Load the previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadOptions()
    {
        // Load the options scene
        SceneManager.LoadScene("Options");
    }

    public void LoadCredits()
    {
        // Load the credits scene
        SceneManager.LoadScene("Credits");
    }

    public void LoadLevelSelect()
    {
        // Load the level select scene
        SceneManager.LoadScene("LevelSelect");
    }
}
