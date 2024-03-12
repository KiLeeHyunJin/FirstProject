using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBasicAttack0 : SkillState
{
    protected override void Attack(float normalTime)
    {
        if(owner.keys.ContainLayer(KeyManager.DefaultKey.X))
            skillController.Click = true;
    }

    protected override void EnterAction()
    {
    }

    protected override void ExitAction()
    {
        if (owner.CurrentState != PlayerState.LandAtck1)
            skillController.Out();
    }

    protected override PlayerState NextAnim()
    {
        return PlayerState.LandAtck1;
    }

}
