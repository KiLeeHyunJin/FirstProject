using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockDownAttack : AttackSkill
{
    public override void InputKeyCount()
    {
        anim.Play(animId[0]);
        if (animCo != null)
            StopCoroutine(animCo);
        animCo = StartCoroutine(AnimationInputSensor(inputCount));
        Attack(inputCount);
    }

    protected Coroutine animCo = null;
    protected IEnumerator AnimationInputSensor(int input)
    {
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackData[0].AnimName))
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    break;
            yield return new WaitForFixedUpdate();
        }
        anim.Play("Idle");
    }
}
