using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Idle : BaseState<GoblinState>
{

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.IdleId);
    }
    public override void Update()
    {
    }
    public override void Transition()
    {
    }
}
