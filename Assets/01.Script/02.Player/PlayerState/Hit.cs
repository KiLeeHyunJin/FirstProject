using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hit : BaseState<PlayerController.State>
{
    [SerializeField] public float delay;
    public void SetDelayTime(float time) => delay = time;

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
        coroutine = owner.StartCoroutine(WaitCoroutine());
        int idx = UnityEngine.Random.Range(0, 2);
        if (idx == 1)
            anim.Play(AnimIdTable.GetInstance.Hit1Id);
        else
            anim.Play(AnimIdTable.GetInstance.Hit2Id);
    }
    Coroutine coroutine = null;
    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(delay);
        owner.SetState = PlayerController.State.Idle;
    }
    public override void Exit()
    {
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        if (isEnter == false)
            return;
        //coroutine = owner.StartCoroutine(WaitCoroutine());
    }

    public override void Transition()
    {
    }
}
