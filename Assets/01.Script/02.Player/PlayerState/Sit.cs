using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sit : PlayerBaseState<PlayerState>
{
    [SerializeField] float readyTime;
    [SerializeField] float defaultTime;
    bool isTransition;
    bool isReady;
    public override void Enter()
    {
        isTransition = false;
        isReady = false;
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
        anim.Play(AnimIdTable.GetInstance.SitId);
    }
    Coroutine coroutine = null;
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(defaultTime);
        isReady = true;
        yield return new WaitForSeconds(readyTime - defaultTime);
        isTransition = true;
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
            owner.SetState = PlayerState.Idle;
        }
        else if (isReady)
        {
            owner.SetStandState = StandingState.Stand;
            if (
                owner.keys.ContainLayer(KeyManager.DefaultKey.Move))
                owner.SetState = PlayerState.Walk;
            else if (
                owner.keys.ContainLayer(KeyManager.DefaultKey.C))
                owner.SetState = PlayerState.JumpUp;
            else if (
                owner.keys.ContainLayer(KeyManager.DefaultKey.X))
                owner.SetState = PlayerState.X;
        }

    }
}
