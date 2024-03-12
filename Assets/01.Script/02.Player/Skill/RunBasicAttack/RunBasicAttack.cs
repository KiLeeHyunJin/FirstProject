using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBasicAttack : SkillState
{
    protected override void Attack(float normalTime)
    {
    }

    protected override void EnterAction()
    {
    }

    protected override void ExitAction()
    {
        skillController.Out();
    }

    protected override PlayerState NextAnim()
    {
        return PlayerState.Idle;
    }

}
