using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[Serializable]
public class JumpUp : BaseState<PlayerController.State>
{
    [SerializeField] Vector2 moveSpeed;
    [SerializeField] float jumpPower;
    bool isTransition;

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.JumpUpId);
        isTransition = false;
        if (pos.yState() == TransformAddForce.YState.None || pos.Y <= 0)
            pos.AddForce(new Vector3(0, jumpPower, 0));
    }


    public override void Update()
    {
        Vector2 moveValue = owner.moveValue;
        owner.FlipCheck();
        pos.AddForceMove(moveValue * moveSpeed);
    }

    public override void FixedUpdate()
    {
        //pos.JumpingFreezePosition();
        if (pos.yState() == TransformAddForce.YState.Down || pos.Velocity().y < 0)
            isTransition = true;
    }
    public override void Transition()
    {
        if (isTransition)
            owner.SetState = PlayerController.State.JumpDown;
        else if (owner.keys.ContainLayer(KeyManager.Key.X))
            if (pos.Y > 0.5f)
                owner.SetState = PlayerController.State.Interaction;
    }


}
