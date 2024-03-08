using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Idle : MonsterState<TauState>
{
    [SerializeField]
    Vector2 duringTime;
    bool isTransition;
    float idleTime;

    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.IdleId);
        isTransition = false;
        idleTime = UnityEngine.Random.Range(duringTime.x, duringTime.y);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(waitCo());
    }
    Coroutine coroutine = null;
    IEnumerator waitCo()
    {
        yield return new WaitForSeconds(idleTime);
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
            owner.SetState = TauState.Walk;
    }

}
