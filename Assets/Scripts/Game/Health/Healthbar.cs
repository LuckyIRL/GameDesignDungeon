using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Slider _healthSlider;

    void Awake()
    {
        _healthSlider = GetComponent<Slider>();
        if (_healthSlider == null)
        {
            Debug.LogError("Slider component not found!");
        }
    }

    public void SetMaxHealth(int health)
    {
        _healthSlider.maxValue = health;
        _healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        _healthSlider.value = health;
    }
}
