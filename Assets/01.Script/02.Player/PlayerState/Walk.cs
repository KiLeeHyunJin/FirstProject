using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[Serializable]
public class Walk : PlayerBaseState<PlayerState>
{
    [SerializeField] Vector2 Speed;
    [SerializeField] float term;
    float enterTime;
    public override void Enter() 
    {
        float beforeEnterTime = enterTime;
        enterTime = Time.time;
        if(enterTime - beforeEnterTime < term)
            playerOwner.SetState = PlayerState.Run;
        else
        {
            anim.Play(AnimIdTable.GetInstance.WalkId);
            playerOwner.WalkType = PlayerState.Walk;
        }
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
