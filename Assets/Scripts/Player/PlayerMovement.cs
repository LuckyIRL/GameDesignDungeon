using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public InputManager inputManager;

    public Rigidbody rb;

    public float moveSpeed = 10f;
    public float sprintSpeed = 15f;

    public float jumpForce = 200f;

    private bool isGrounded;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        inputManager.inputMaster.Movement.Jump.started += _ => Jump();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardMovement = inputManager.inputMaster.Movement.Forward.ReadValue<float>();
        float rightMovement = inputManager.inputMaster.Movement.Right.ReadValue<float>();
        Vector3 move = transform.right * rightMovement + transform.forward * forwardMovement;

        move *= inputManager.inputMaster.Movement.Sprint.ReadValue<float>() == 0 ? moveSpeed : sprintSpeed;
        transform.localScale = new Vector3(1, inputManager.inputMaster.Movement.Crouch.ReadValue<float>() == 0 ? 1 : 0.72610f, 1);

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void Jump() 
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }
    }
}
