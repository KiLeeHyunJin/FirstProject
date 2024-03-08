using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Atck2 : MonsterState<TauState> //µ¹Áø
{
    bool isTransition;

    public override void Enter()
    {
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.Atck2Id);
    }
    public override void Update()
    {
    }

    public override void FixedUpdate()
    {
        owner.FlipCheck(pos.Velocity2D());
    }

    public override void Exit()
    {
    }

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = TauState.AtckFinish;
    }

}
