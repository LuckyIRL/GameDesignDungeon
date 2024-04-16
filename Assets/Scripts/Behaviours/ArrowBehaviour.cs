using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public int damage = 1;
    public int time = 0;
    public float force = 10f;

    // Move the arrow forward and rotate it
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    // Destroy the arrow after a certain amount of time
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Deal damage to the enemy

}
