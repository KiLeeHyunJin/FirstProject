using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class HeatWaveSlash : AttackState
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
        isTransition = false;
        Attack();
    }
}
