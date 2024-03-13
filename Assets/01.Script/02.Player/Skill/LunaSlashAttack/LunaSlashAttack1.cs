using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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
        owner.StartCoroutine(LunaShootCo());
    }
    IEnumerator LunaShootCo()
    {
        LunaSlashController controller = skillController as LunaSlashController;
        if (controller == null)
            yield break;
        yield return new WaitForSeconds(attackData.attackCounts[0].AttackTime);
        float scale = 0.7f + (controller.ChargeTime * 0.3f);
        if (scale > 1.5f)
            scale = 1.5f;
        Vector3 position = pos.Pose + new Vector3(0.5f * direction, 0, 0);
        Vector2 power = attackData.attackCounts[0].power;
        power.x *= direction;


        controller.luna.SetData(power, position, direction);
        controller.luna.SetData(
            controller.ChargeTime * 3,
            pos.Pose,
            attackData.attackSize.size * scale,
            attackData.attackSize.offset * scale,
            attackData.attackCounts[0].effectType);
        controller.luna.transform.localScale = Vector3.one * scale;
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
