using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBasicAttack : SkillState
{
    protected override void Attack(float normalTime)
    {
    }

    protected override void EnterAction()
    {
        if (pos.Velocity().y < 0)
            pos.AddForce(new Vector3(0, 0.2f, 0));
    }

    protected override void ExitAction()
    {
        skillController.Out();
    }

    protected override PlayerState NextAnim()
    {
        PlayerState state;
        if (pos.yState() != TransformAddForce.YState.None)
        {
            if (pos.Velocity().y > 0)
                state = PlayerState.JumpUp;
            else
                state = PlayerState.JumpDown;
        }
        else
            state = PlayerState.Idle;
        return state;
    }

}
