using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShortAirSlash : AttackState
{
    public override void Transition()
    {
        if (isTransition)
            owner.SetState = PlayerState.Idle;
    }

    protected override void AttackEffect()
    {
    }
    public override void Enter()
    {
        pos.SetDirection = owner.moveValue.x == 1 ? TransformPos.Direction.Right : TransformPos.Direction.Left;
        isTransition = false;
        Attack();
    }
}
