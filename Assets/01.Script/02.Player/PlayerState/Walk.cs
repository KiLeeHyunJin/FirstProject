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
            owner.SetState = PlayerState.Run;
        else
        {
            anim.Play(AnimIdTable.GetInstance.WalkId);
            owner.WalkType = PlayerState.Walk;
        }
    }
    public override void Update()
    {
        Vector2 moveValue = owner.moveValue;
        owner.FlipCheck(moveValue);
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
        if (owner.keys.ContainLayer(KeyManager.DefaultKey.C))
            owner.SetState = PlayerState.JumpUp;
        else if (
            owner.keys.ContainLayer(KeyManager.DefaultKey.X))
            owner.SetState = PlayerState.BasicAtck;
        else if (
            owner.keys.ContainLayer(KeyManager.DefaultKey.Move) == false)
            owner.SetState = PlayerState.Idle;
    }
}
