using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_AtckReady1 : MonsterState<TauState>
{
    [SerializeField]
    float reayTime;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.AtckReady1Id);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        owner.StartCoroutine(WaitCo());
    }
    Coroutine coroutine = null;
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(reayTime);
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
            owner.SetState = TauState.Atck3;
    }

}
