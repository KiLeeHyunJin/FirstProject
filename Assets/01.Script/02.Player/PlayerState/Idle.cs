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
        isAlerted = owner.isAlert;
        if (isAlerted)
        {
            anim.Play(AnimIdTable.GetInstance.AlertId);
        }
        else
            anim.Play(AnimIdTable.GetInstance.IdleId);
        owner.WalkType = PlayerState.Idle;
    }
    public override void Update()
    {
        if(isAlerted != owner.isAlert)
        {
            anim.Play(AnimIdTable.GetInstance.IdleId);
            isAlerted = owner.isAlert;
        }
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }

    public override void Transition()
    {
        if (owner.keys.ContainLayer(KeyManager.DefaultKey.Move))
            owner.SetState = PlayerState.Walk;
        else if
            (owner.keys.ContainLayer(KeyManager.DefaultKey.C))
            owner.SetState = PlayerState.JumpUp;
        else if
            (owner.keys.ContainLayer(KeyManager.DefaultKey.X))
            owner.SetState = PlayerState.X;
    }
}
