using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BasicAttack : PlayerBaseState<PlayerState>
{
    [SerializeField] float jumpLimitYPos;
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
            if (owner.WalkType == PlayerState.Run)
                owner.SetState = PlayerState.RunAtckControll;
            else
                owner.SetState = PlayerState.LandAtckControll;
            owner.WalkType = PlayerState.Idle;
        }
        else
        {
            if (pos.Y > jumpLimitYPos)
                owner.SetState = PlayerState.JumpAtckControll;
            else
                owner.SetState = PlayerState.JumpUp;
        }
    }
}
