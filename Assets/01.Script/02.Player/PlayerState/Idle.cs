using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Idle : BaseState<PlayerController.State>
{
    public override void Enter() 
    {
        anim.Play(AnimIdTable.GetInstance.IdleId);
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
            owner.SetState = PlayerController.State.Jump;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.X))
            owner.SetState = PlayerController.State.Interaction;
    }
}
