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
        controller.luna.SetData(controller.ChargeTime * 2);
        controller.luna.SetData(attackData.attackCounts[0].power, pos.Pose, direction);
        controller.luna.transform.localScale = Vector3.one * scale;


        
        //controller. projectile. 
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
