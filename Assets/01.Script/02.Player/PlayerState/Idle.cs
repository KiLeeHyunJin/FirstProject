using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Idle : BaseState<PlayerController.State>
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
        owner.WalkType = PlayerController.State.Idle;
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
        if (owner.keys.ContainLayer(KeyManager.Key.Move))
            owner.SetState = PlayerController.State.Walk;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.C))
            owner.SetState = PlayerController.State.JumpUp;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.X))
            owner.SetState = PlayerController.State.Interaction;
    }
}
