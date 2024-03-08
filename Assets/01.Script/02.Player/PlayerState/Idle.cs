using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Idle : PlayerBaseState<PlayerState>
{
    bool isAlerted;
    public override void Enter() 
    {
        isAlerted = playerOwner.isAlert;
        if (isAlerted)
        {
            anim.Play(AnimIdTable.GetInstance.AlertId);
        }
        else
            anim.Play(AnimIdTable.GetInstance.IdleId);
        playerOwner.WalkType = PlayerState.Idle;
    }
    public override void Update()
    {
        if(isAlerted != playerOwner.isAlert)
        {
            anim.Play(AnimIdTable.GetInstance.IdleId);
            isAlerted = playerOwner.isAlert;
        }
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }

    public override void Transition()
    {
        if (playerOwner.keys.ContainLayer(KeyManager.Key.Move))
            playerOwner.SetState = PlayerState.Walk;
        else if
            (playerOwner.keys.ContainLayer(KeyManager.Key.C))
            playerOwner.SetState = PlayerState.JumpUp;
        else if
            (playerOwner.keys.ContainLayer(KeyManager.Key.X))
            playerOwner.SetState = PlayerState.Interaction;
    }
}
