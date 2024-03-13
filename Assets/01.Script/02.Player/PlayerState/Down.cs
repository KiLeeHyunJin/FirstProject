using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Down : PlayerBaseState<PlayerState>
{
    [SerializeField] float downTime;
    bool isTransition;
    public override void Enter() 
    {
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.DownId);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitTime());
        owner.SetStandState = StandingState.Down;
    }
    Coroutine coroutine = null;
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(downTime);
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
        pos.ForceZero(KeyCode.X);
    }

    public override void Transition()
    {
        if (owner.isDie)
            return;
        if (isTransition)
        {
            owner.SetStandState = StandingState.Sit;
            owner.SetState = PlayerState.Sit;
        }
    }
}
