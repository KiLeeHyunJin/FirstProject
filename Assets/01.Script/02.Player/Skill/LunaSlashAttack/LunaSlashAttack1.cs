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
        Vector2 position = new Vector2(0.5f * direction, 0);
        Vector2 power = attackData.attackCounts[0].power;
        power.x *= direction;
        controller.luna.SetAttackData(power, attackData.damage, attackData.attackCounts[0].effectType, attackData.attackCounts[0].stunTime, attackData.attackCounts[0].pushTime);
        controller.luna.SetDirection(direction);
        controller.luna.SetPosition(pos.Pose, attackData.attackSize.size * scale, attackData.attackSize.offset * scale, position);
        controller.luna.SetState(true, 0.25f,controller.ChargeTime * 3.5f);
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
