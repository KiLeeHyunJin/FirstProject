using System;
using UnityEngine;

[Serializable]
public class JumpUp : PlayerBaseState<PlayerState>
{
    [SerializeField] Vector2 moveWalkSpeed;
    [SerializeField] Vector2 moveRunSpeed;
    [SerializeField] float jumpPower;
    bool isTransition;
    Vector2 speed;
    public override void Enter()
    {
        base.Enter();
        anim.Play(AnimIdTable.GetInstance.JumpUpId);
        isTransition = false;
        if (pos.yState() == TransformAddForce.YState.None || pos.Y <= 0)
            pos.AddForce(new Vector3(0, jumpPower, 0));
        if (owner.WalkType == PlayerState.Walk)
            speed = moveWalkSpeed;
        else
            speed = moveRunSpeed;
    }

    public override void Update()
    {
        Vector2 moveValue = owner.moveValue;
        owner.FlipCheck(moveValue);
        pos.AddForceMove(moveValue * speed);
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
            owner.SetState = PlayerState.JumpDown;
        else if (owner.keys.ContainLayer(KeyManager.DefaultKey.X))
            if (pos.Y > 0.5f)
                owner.SetState = PlayerState.X;

    }


}
