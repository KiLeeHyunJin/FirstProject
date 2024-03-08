using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Walk : BaseState<GoblinState>
{
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.WalkId);
    }
    public override void Update()
    {
        owner.ta
    }
    public override void Transition()
    {
    }
}
