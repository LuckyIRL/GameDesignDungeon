using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimHipFireState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.Anim.SetBool("Aiming", false);
        aim.currentFOV = aim.hipFOV;
    }

    public override void UpdateState(AimStateManager aim) 
    {
        if (Input.GetKey(KeyCode.Mouse1)) aim.SwitchState(aim.Aim);
    }
}
