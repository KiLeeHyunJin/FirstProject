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
        playerOwner.OnStartAlert();
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
            if (playerOwner.WalkType == PlayerState.Run)
                playerOwner.SetState = PlayerState.RunAtck;
            else
                playerOwner.SetState = PlayerState.LandAtck;
            playerOwner.WalkType = PlayerState.Idle;
        }
        else
        {
            if (pos.Y > jumpLimitYPos)
                playerOwner.SetState = PlayerState.JumpAtck;
            else
                playerOwner.SetState = PlayerState.JumpUp;
        }
    }
}
