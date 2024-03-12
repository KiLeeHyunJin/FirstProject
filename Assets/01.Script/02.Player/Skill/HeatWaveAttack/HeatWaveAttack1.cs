using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatWaveAttack1 : SkillState
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
        skillController.Out();
        return PlayerState.Idle;
    }

}
