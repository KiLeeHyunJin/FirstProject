using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeatWaveAttackController : SkillStateController
{
    protected override void EnterState()
    {
        owner.activeType = ActiveType.Skill;
        owner.SetState = PlayerState.HeatWaveAttack0;
    }
}
