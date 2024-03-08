using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Atck : BaseState<GoblinState>
{

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.AtckId);
    }
    public override void Update()
    {
    }
    public override void Transition()
    {
    }
}
