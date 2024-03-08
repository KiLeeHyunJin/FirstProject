using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JumpAttack : AttackState
{
    public override void Enter()
    {
        isTransition = false;
        if(pos.Velocity().y < 0)
            pos.AddForce(new Vector3(0, 0.2f, 0));
        Attack();
    }
    public override void Transition()
    {
        if(isTransition)
        {
            if (pos.yState() != TransformAddForce.YState.None)
            {
                if (pos.Velocity().y > 0)
                    owner.SetState = PlayerState.JumpUp;
                else
                    owner.SetState= PlayerState.JumpDown;
            }
            else
                owner.SetState = PlayerState.Idle;
        }
    }

    protected override void AttackEffect()
    {
    }


}
