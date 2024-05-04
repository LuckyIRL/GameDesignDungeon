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
        yield return new WaitForSeconds(delay);
        // Reset the player's health
        _playerHealth.Health = _playerHealth.MaxHealth;
        // Update the health bar
        playerBehaviour._healthbar.SetHealth(_playerHealth.Health);
        // Set the player's position to the starting position
        GameObject.FindGameObjectWithTag("Player").transform.position = checkpointPos;
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



}
