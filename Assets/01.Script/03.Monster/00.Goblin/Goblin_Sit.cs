using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Sit : MonsterState<GoblinState>
{
    [SerializeField] float sittingTime;
    bool isTransition;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.SitId);
        isTransition = false;
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    Coroutine coroutine = null;
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(sittingTime);
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
        if(isTransition)
        {
            owner.SetState = GoblinState.Idle;
        }
    }
}
