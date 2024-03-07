using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Alert : BaseState<PlayerController.State>
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
            owner.SetState = PlayerController.State.Walk;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.C))
            owner.SetState = PlayerController.State.JumpUp;
        else if
            (owner.keys.ContainLayer(KeyManager.Key.X))
            owner.SetState = PlayerController.State.Interaction;
    }
}
