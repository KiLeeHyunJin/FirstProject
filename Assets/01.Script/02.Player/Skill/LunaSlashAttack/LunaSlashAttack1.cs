using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaSlashAttack1 : SkillState
{

    protected override void Attack(float normalTime)
    {

    }

    protected override void EnterAction()
    {
        LunaSlashController controller = skillController as LunaSlashController;
        if (controller == null)
            return;
        float scale = 0.7f + (controller.ChargeTime * 0.3f);
        if (scale > 1.5f)
            scale = 1.5f;


        
        //controller. projectile. 
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
