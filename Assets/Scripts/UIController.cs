using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas mainScreenUI;
    [SerializeField] private Canvas PauseUI;
    private StarterAssetsInputs inputs;

    public Slider musicSlider, sfxSlider;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        inputs = GetComponent<StarterAssetsInputs>();
    }

    // Pause the game
    public void PauseGame()
    {
        if (inputs.isPaused)
        {
            Time.timeScale = 1;
            SwitchUI();
        }
        else
        {
            Time.timeScale = 0;
            SwitchUI();
        }
    }

    // Switch between the main screen UI and the pause UI
    private void SwitchUI()
    {
        mainScreenUI.gameObject.SetActive(!mainScreenUI.gameObject.activeSelf);
        PauseUI.gameObject.SetActive(!PauseUI.gameObject.activeSelf);
        inputs.isPaused = !inputs.isPaused;
    }





    // Update the music volume
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    // Update the SFX volume
    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }

    // Update the music volume
    public void SetMusicVolume()
    {
        AudioManager.instance.SetMusicVolume(musicSlider.value);
    }

    // Update the SFX volume
    public void SetSFXVolume()
    {
        AudioManager.instance.SetSFXVolume(sfxSlider.value);
    }
}
