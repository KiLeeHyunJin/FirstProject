using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LunaSlashAttack0 : SkillState
{
    KeyCode sensorKey;
    float time;
    protected override void Attack(float normalTime)
    {
        if(Input.GetKeyUp(sensorKey))
        {
            nextAnim = true;
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    protected override void EnterAction()
    {
        time = 0;
        sensorKey  = EnumUtil<KeyCode>.Parse(skillController.currentSkillKey.ToString());
    }

    protected override void ExitAction()
    {
        LunaSlashController controller = skillController as LunaSlashController;
        if(controller != null)
            controller.ChargeTime = time;
    }

    protected override PlayerState NextAnim()
    {
        return PlayerState.LunaSlashAttack1;
    }
}
