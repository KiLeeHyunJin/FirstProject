using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Land : PlayerBaseState<PlayerState>
{
    [Range(0,0.5f)]
    [SerializeField]
    float delayTime;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(DelayCo());
        pos.ForceZero();
    }
    Coroutine coroutine = null;
    IEnumerator DelayCo()
    {
        anim.Play("Jump_Land");
        yield return new WaitForSeconds(delayTime);
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
        {
            if(owner.WalkType == PlayerState.Run)
                owner.SetState = PlayerState.Run;
            else if(owner.WalkType == PlayerState.Walk)
                owner.SetState = PlayerState.Walk;
            else
                owner.SetState = PlayerState.Idle;

        }
    }
}
