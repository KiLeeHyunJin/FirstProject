using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Run : PlayerBaseState<PlayerState>
{

    [SerializeField] Vector2 Speed;

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.RunId);
        playerOwner.WalkType = PlayerState.Run;
    }
    public override void Update()
    {
        Vector2 moveValue = playerOwner.moveValue;
        playerOwner.FlipCheck(moveValue);
        pos.AddForceMove(moveValue * Speed);
    }

    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    public override void Exit()
    {
        pos.ForceZero(KeyCode.X);
    }

    public override void Transition()
    {
        if (playerOwner.keys.ContainLayer(KeyManager.Key.C))
            playerOwner.SetState = PlayerState.JumpUp;
        else if (
            playerOwner.keys.ContainLayer(KeyManager.Key.X))
            playerOwner.SetState = PlayerState.BasicAtck;
        else if (
            playerOwner.keys.ContainLayer(KeyManager.Key.Move) == false)
            playerOwner.SetState = PlayerState.Idle;
    }
}
