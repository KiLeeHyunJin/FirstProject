using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Idle : BaseState<PlayerController.State>
{
    protected override void EnterCheck()
    {
        return;
    }
    public override void Enter() 
    {
        if (pos.yState() != TransformAddForce.YState.None)
            owner.SetState = PlayerController.State.Jump;
        else if (pos.Velocity().magnitude > 0)
            owner.SetState = PlayerController.State.Walk;
    }
}
