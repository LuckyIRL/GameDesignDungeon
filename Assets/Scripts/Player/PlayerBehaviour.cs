using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public Healthbar _healthbar;
    public float arrowsCollected;
    private Vector3 startpointPos;
    //private MeshRenderer playerMesh;

    void Start()
    {
        // Set the initial health of the player in the health bar
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);
        startpointPos = transform.position;
    }

    // Method to take damage when hit by the boss
    public void TakeDamage(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        _healthbar.SetHealth(GameManager.gameManager._playerHealth.Health);

        // Check if player's health reaches zero
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            //MeshRenderer playerMesh = GetComponent<MeshRenderer>();
            //playerMesh.enabled = false;
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.Respawn(.5f));
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
