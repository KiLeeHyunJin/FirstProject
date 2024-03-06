using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Down : BaseState<PlayerController.State>
{
    [SerializeField] float downTime;
    protected override void EnterCheck()
    {
        isEnter = true;
    }
    public override void Enter() 
    {
        EnterCheck();
        if (isEnter == false)
            return;
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitTime());
        anim.Play("Falling");
    }
    Coroutine coroutine = null;
    IEnumerator WaitTime()
    {
        while(pos.yState() != TransformAddForce.YState.None)
        {
            yield return new WaitForFixedUpdate();
        }
        anim.Play("Down");
        yield return new WaitForSeconds(downTime);
        owner.SetState = PlayerController.State.Idle;
    }
    public override void Exit() 
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }
}
