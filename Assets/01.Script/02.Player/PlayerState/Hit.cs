using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hit : BaseState<PlayerController.State>
{
    [SerializeField] public float delay;
    public void SetDelayTime(float time) => delay = time;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;

        if (coroutine != null)
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
        if(isTransition)
            owner.SetState = PlayerController.State.Idle;
    }
}
