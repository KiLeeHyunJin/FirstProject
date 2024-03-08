using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Alert : PlayerBaseState<PlayerState>
{
    // Start is called before the first frame update
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.AlertId);

    }


    // Update is called once per frame
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    public override void Transition()
    {
        if (owner.keys.ContainLayer(KeyManager.Key.Move))
            owner.SetState = PlayerState.Walk;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.C))
            owner.SetState = PlayerState.JumpUp;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.X))
            owner.SetState = PlayerState.Interaction;
    }
}
