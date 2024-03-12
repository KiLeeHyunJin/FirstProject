using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackState;
[Serializable]
public class SkillState : PlayerBaseState<PlayerState>
{
    public SkillStateController skillController;
    public AttackData attackData;
    public int animId;
    protected bool isTransition;
    public bool chaingAnim;
    public override void Enter()
    {
    }
    public override void Update()
    {
    }

    public override void Transition()
    {
    }
}
