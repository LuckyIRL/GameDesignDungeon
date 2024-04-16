using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    // Variables and Methods to add force to the arrow's direction and speed and target
    public float _speed = 10.0f;
    private Vector3 forwardDirection;
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void Awake()
    {
        forwardDirection = transform.forward;
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }
        transform.position += forwardDirection * _speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            hit = true;
        }
    }


}
