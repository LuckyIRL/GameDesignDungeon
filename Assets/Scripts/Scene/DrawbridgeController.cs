using System.Collections.Generic;
using UnityEngine;

public class DrawbridgeController : MonoBehaviour
{
    // Mapping between trigger objects and their respective drawbridges
    public Dictionary<Collider, Animator> triggerToDrawbridgeMap = new Dictionary<Collider, Animator>();

    // Add a trigger and its corresponding drawbridge to the mapping
    public void AddDrawbridge(Collider trigger, Animator drawbridgeAnimator)
    {
        triggerToDrawbridgeMap.Add(trigger, drawbridgeAnimator);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the arrow and if it exists in the mapping
        if (other.CompareTag("Arrow") && triggerToDrawbridgeMap.ContainsKey(other))
        {
            // Trigger the corresponding drawbridge lowering animation
            Animator drawbridgeAnimator = triggerToDrawbridgeMap[other];
            drawbridgeAnimator.SetTrigger("LowerDrawbridge");
        }
    }
}

