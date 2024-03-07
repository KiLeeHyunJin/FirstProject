using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : BaseState<PlayerController.State>
{
    public override void Enter()
    {
        owner.OnStartAlert();
    }
    public override void FixedUpdate()
    {
        if(pos.yState() == TransformAddForce.YState.None)
            pos.Synchro();
    }
    public override void Transition()
    {
        if (pos.yState() == TransformAddForce.YState.None)
        {
            owner.SetState = PlayerController.State.Idle;
            if (owner.WalkType == PlayerController.State.Run)
            {
                return;//owner.SetState = PlayerController.State.Stap;
            }
            else
                owner.SetState = PlayerController.State.LandAttack;
        }
        else
        {
            if (pos.Y > 0.5f)
                owner.SetState = PlayerController.State.JumpAtck;
            else
                owner.SetState = PlayerController.State.JumpUp;
        }
    }
}
