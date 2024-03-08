using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Hit : BaseState<GoblinState>
{

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.Hit1Id);
    }
    public override void Update()
    {
    }
    public override void Transition()
    {
    }
}
