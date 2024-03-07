using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JumpAttack : AttackState
{
    public override void Transition()
    {
        if(isTransition)
        {
            if (pos.yState() != TransformAddForce.YState.None)
            {
                if (pos.Velocity().y > 0)
                {
                    owner.SetState = PlayerController.State.JumpUp;
                }
                else
                {
                    owner.SetState= PlayerController.State.JumpDown;
                }
            }
            else
                owner.SetState = PlayerController.State.Idle;
        }
    }

    protected override void AttackEffect()
    {
    }

    public override void Enter()
    {
        isTransition = false;
        //anim.Play(animId[0]);
        pos.AddForce(new Vector3(0, 0.2f, 0));
        Attack();
    }

}
