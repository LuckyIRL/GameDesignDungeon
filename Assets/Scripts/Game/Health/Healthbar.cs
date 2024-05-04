using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Slider _healthSlider;

    void Start()
    {
        _healthSlider = GetComponent<Slider>();
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
