using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Run : BaseState<PlayerController.State>
{

    [SerializeField] Vector2 Speed;

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.RunId);
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
            owner.SetState = PlayerController.State.Jump;
        else if (
            owner.keys.ContainLayer(KeyManager.Key.X))
            owner.SetState = PlayerController.State.BasicAtck;
        else if (
            owner.keys.ContainLayer(KeyManager.Key.Move) == false)
            owner.SetState = PlayerController.State.Idle;
    }
}
