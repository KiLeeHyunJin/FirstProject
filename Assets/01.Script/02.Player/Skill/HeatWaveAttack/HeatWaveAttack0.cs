using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWaveAttack0 : SkillState
{
    protected override void Attack(float normalTime)
    {
    }

    protected override void EnterAction()
    {
        owner.activeType = ActiveType.Skill;
    }

    protected override void ExitAction()
    {
        if (owner.CurrentState != PlayerState.HeatWaveAttack1)
        {
            skillController.Out();
        }
    }

    protected override PlayerState NextAnim()
    {
        return PlayerState.HeatWaveAttack1;
    }
}
