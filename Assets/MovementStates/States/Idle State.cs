using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {

    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.moveDirection.magnitude > 0.1f) 
        {
            if (Input.GetKey(KeyCode.LeftShift))movement.SwitchState(movement.run);
            else movement.SwitchState(movement.walk);

        }

        if (Input.GetKey(KeyCode.C)) movement.SwitchState(movement.crouch);
    }
}
