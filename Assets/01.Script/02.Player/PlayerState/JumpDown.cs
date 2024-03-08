using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JumpDown : PlayerBaseState<PlayerState>
{
    [SerializeField] Vector2 moveWalkSpeed;
    [SerializeField] Vector2 moveRunSpeed;
    bool isTransition;
    Vector2 speed;

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = PlayerState.Land;
        else if (owner.keys.ContainLayer(KeyManager.Key.X))
            if(pos.Y > 0.5f)
                owner.SetState = PlayerState.Interaction;
    }

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.JumpDownId);
        isTransition = false;
        if (owner.WalkType == PlayerState.Walk)
            speed = moveWalkSpeed;
        else
            speed = moveRunSpeed;
    }

    public override void Update()
    {
        Vector2 moveValue = owner.moveValue;
        owner.FlipCheck();
        pos.AddForceMove(moveValue * speed);
    }
    public override void FixedUpdate()
    {
        //pos.JumpingFreezePosition();
        if (pos.yState() == TransformAddForce.YState.None || pos.Y <= 0)
            isTransition = true;
    }

}
