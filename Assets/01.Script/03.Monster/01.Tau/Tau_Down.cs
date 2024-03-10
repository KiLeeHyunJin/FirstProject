using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Down : MonsterState<TauState>
{
    [SerializeField] float downTime;
    bool isTransition;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.DownId);
        isTransition = false;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
        owner.SetStandState = BaseController<TauState>.StandingState.Down;
    }
    Coroutine coroutine = null;
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(downTime);
        isTransition = true;
    }
    public override void Exit()
    {
        base.Exit();
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }
    public override void Transition()
    {
        if (isTransition)
        {
            owner.SetState = TauState.Idle;
            owner.SetStandState = BaseController<TauState>.StandingState.Stand;
        }
    }
}
