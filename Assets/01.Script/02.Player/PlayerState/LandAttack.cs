using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LandAttack : AttackState
{
    public override void Transition()
    {
        if (isTransition)
        {
            owner.SetState = PlayerState.Idle;
            return;
        }
        else if (owner.keys.ContainLayer(KeyManager.Key.X))
            Attack();
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
