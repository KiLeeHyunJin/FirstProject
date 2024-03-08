using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Down : MonsterState<GoblinState>
{
    [SerializeField] float downTime;
    bool isTransition;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.DownId);
        isTransition = false;
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
    }
    Coroutine coroutine = null;
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(downTime);
        isTransition = true;
    }
    public override void Exit()
    {
        base.Exit();
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
    }
    public override void Transition()
    {
        if (isTransition)
            owner.SetState = GoblinState.Sit;
    }
}
