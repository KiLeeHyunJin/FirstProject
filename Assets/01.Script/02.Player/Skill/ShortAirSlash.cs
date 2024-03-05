using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortAirSlash : AttackSkill
{
    public override void InputKeyCount()
    {
        if (animId.Length > 1)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackData[inputCount].AnimName))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackData[inputCount].delay)
                {
                    inputCount++;
                    if (animId.Length <= inputCount)
                    {
                        inputCount = 0;
                    }
                }
                else
                    return;
            }
            else
            {
                inputCount = 0;
            }
            anim.Play(animId[inputCount]);
            if (animCo != null)
                StopCoroutine(animCo);
            animCo = StartCoroutine(AnimationInputSensor(inputCount));
            Attack(inputCount);
        }
    }
    protected Coroutine animCo = null;
    protected IEnumerator AnimationInputSensor(int input)
    {
        int idx = input;
        while (true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(attackData[inputCount].AnimName))
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    break;
            yield return new WaitForFixedUpdate();
        }
        anim.Play("Idle");
    }
}
