using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
[Serializable]
public class Walk : BaseState<PlayerController.State>
{
    [SerializeField] Vector2 Speed;
    [SerializeField] float term;
    float enterTime;
    public override void Enter() 
    {
        float beforeEnterTime = enterTime;
        enterTime = Time.time;
        if(enterTime - beforeEnterTime < term)
            owner.SetState = PlayerController.State.Run;
        else
        {
            anim.Play(AnimIdTable.GetInstance.WalkId);
            owner.WalkType = PlayerController.State.Walk;
        }
    }
    public override void Update()
    {
        Vector2 moveValue = owner.moveValue;
        owner.FlipCheck();
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
        if (owner.keys.ContainLayer(KeyManager.Key.C))
            owner.SetState = PlayerController.State.JumpUp;
        else if (
            owner.keys.ContainLayer(KeyManager.Key.X))
            owner.SetState = PlayerController.State.BasicAtck;
        else if (
            owner.keys.ContainLayer(KeyManager.Key.Move) == false)
            owner.SetState = PlayerController.State.Idle;
    }
}
