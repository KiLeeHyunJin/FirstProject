using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Down : BaseState<GoblinState>
{

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.DownId);
    }
    public override void Update()
    {
    }
    public override void Transition()
    {
    }
}
