using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawStrengthBar : MonoBehaviour
{
    // Draw strength slider
    private Slider _drawStrengthSlider;

    private void Start()
    {
        _drawStrengthSlider = GetComponent<Slider>();
    }

    public void SetMaxDrawStrength(int maxDrawStrength)
    {
        _drawStrengthSlider.maxValue = maxDrawStrength;
        _drawStrengthSlider.value = maxDrawStrength;
    }

    public void SetDrawStrength(float drawStrength)
    {
        _drawStrengthSlider.value = drawStrength;
    }


}
