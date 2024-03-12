using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAirSlash1 : SkillState
{
    protected override void EnterAction()
    {
        pos.SetDirection = owner.moveValue.x > 0 ? TransformPos.Direction.Right : TransformPos.Direction.Left;
    }
    protected override void Attack(float normalTime)
    {

    }

    protected override void ExitAction()
    {
        if (owner.CurrentState != PlayerState.AirSlash2)
        {
            skillController.Out();
        }
    }

    protected override PlayerState NextAnim()
    {
        return PlayerState.AirSlash2;
    }
}

