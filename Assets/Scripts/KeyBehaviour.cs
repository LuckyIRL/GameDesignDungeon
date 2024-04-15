using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    [SerializeField] SwitchBehaviour switchBehaviour;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            switchBehaviour.DoorLockedStatus();
            Destroy(gameObject);
        }
    }
}
