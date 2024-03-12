using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAirSlash2 : SkillState
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
    }

    protected override PlayerState NextAnim()
    {
        skillController.Out();
        return PlayerState.Idle;
    }
}
