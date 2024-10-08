using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Fall : MonsterState<TauState>
{
    bool isTransition;
    public override void Enter()
    {

        base.Enter();
        anim.Play(AnimIdTable.GetInstance.FallingId, 0, 0);
        isTransition = false;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(FallingCo());
        owner.SetStandState = StandingState.Fall;
    }
    Coroutine coroutine = null;
    IEnumerator FallingCo()
    {
        while (pos.Velocity().y > 0)
            yield return new WaitForFixedUpdate();

        while (pos.Y > 0 || pos.yState() != TransformAddForce.YState.None)
            yield return new WaitForFixedUpdate();
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
            owner.SetState = TauState.Down;
    }
}
