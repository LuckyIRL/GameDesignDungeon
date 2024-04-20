using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas mainScreenUI;
    [SerializeField] private Canvas aimingUI;

    [SerializeField] private Slider mainHealthSlider;
    [SerializeField] private Slider aimingHealthSlider;

    [SerializeField] private Slider mainDrawSlider;
    [SerializeField] private Slider aimingDrawSlider;

    [SerializeField] private TextMeshProUGUI mainKeyText;
    [SerializeField] private TextMeshProUGUI aimingKeyText;

    [SerializeField] private TextMeshProUGUI mainArrowText;
    [SerializeField] private TextMeshProUGUI aimingArrowText;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
    }

    // Update UI elements with the given values
    public void UpdateUI(float healthSliderValue, float drawSliderValue, string keyText, string arrowText)
    {
        mainHealthSlider.value = healthSliderValue;
        aimingHealthSlider.value = healthSliderValue;

        mainDrawSlider.value = drawSliderValue;
        aimingDrawSlider.value = drawSliderValue;

        mainKeyText.text = keyText;
        aimingKeyText.text = keyText;

        mainArrowText.text = arrowText;
        aimingArrowText.text = arrowText;
    }

    // Switch between main screen UI and aiming UI
    public void SwitchUI(bool isAiming)
    {
        mainScreenUI.enabled = !isAiming;
        aimingUI.enabled = isAiming;
    }
}
