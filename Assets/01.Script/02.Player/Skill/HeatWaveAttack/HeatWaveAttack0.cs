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
    }

    protected override void ExitAction()
    {
    }

    protected override PlayerState NextAnim()
    {
        return PlayerState.HeatWaveAttack1;
    }
}
