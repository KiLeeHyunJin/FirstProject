using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Hit : MonsterState<TauState>
{
    float stunTime;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        stunTime = owner.StunTime;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
    }
    Coroutine coroutine = null;
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(stunTime);
        isTransition = true;
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }

    public override void Exit()
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = TauState.Idle;
    }

}
