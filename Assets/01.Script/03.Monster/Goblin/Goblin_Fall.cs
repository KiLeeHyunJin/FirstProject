using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Fall : BaseState<GoblinState>
{

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.FallingId);
    }
    public override void Update()
    {
    }
    public override void Transition()
    {
    }
}
