using System.Collections.Generic;
using UnityEngine;

public class DrawbridgeController : MonoBehaviour
{
    public Animator drawbridgeAnimator;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the arrow
        if (other.CompareTag("Arrow"))
        {
            // Trigger the drawbridge lowering animation
            drawbridgeAnimator.SetTrigger("LowerDrawbridge");
        }
    }
}
