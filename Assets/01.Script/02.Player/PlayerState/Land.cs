using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Land : BaseState<PlayerController.State>
{
    [Range(0,0.5f)]
    [SerializeField]
    float delayTime;
    protected override void EnterCheck()
    {
        isEnter = true;
    }
    public override void Enter()
    {
        EnterCheck();
        if (isEnter == false)
            return;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(DelayCo());
    }
    Coroutine coroutine = null;
    IEnumerator DelayCo()
    {
        anim.Play("Jump_Land");
        yield return new WaitForSeconds(delayTime);
        owner.SetState = PlayerController.State.Idle;
    }
    public override void Exit()
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        if (isEnter == false)
            return;

    }

    public override void Transition()
    {
    }
}
