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
