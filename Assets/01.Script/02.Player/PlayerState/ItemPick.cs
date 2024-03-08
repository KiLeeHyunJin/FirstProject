using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemPick : PlayerBaseState<PlayerState>
{
    [SerializeField] float pickTime;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(PickingCo());
        anim.Play(AnimIdTable.GetInstance.SitId);
    }
    Coroutine coroutine = null;
    IEnumerator PickingCo()
    {
        yield return new WaitForSeconds(pickTime);
        isTransition = true;
    }

    public override void FixedUpdate()
    {
        pos.ForceZero();
    }

    public override void Exit()
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }
    public override void Transition()
    {
        if (isTransition)
            owner.SetState = PlayerState.Idle;
    }
}
