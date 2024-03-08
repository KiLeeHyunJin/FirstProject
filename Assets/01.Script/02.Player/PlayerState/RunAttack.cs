using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunAttack : AttackState
{

    public override void Transition()
    {
        if (isTransition)
            playerOwner.SetState = PlayerState.Idle;
    }
    public override void Enter()
    {
        isTransition = false;
        Attack();
    }

    protected override void AttackEffect()
    {
    }
}
