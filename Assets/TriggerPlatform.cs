using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    private PlatformMoving platform;
    
    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<PlatformMoving>();
    }

    private void OnTriggerEnter(Collider other)
    {
        platform.canMove = true;
    }
}
