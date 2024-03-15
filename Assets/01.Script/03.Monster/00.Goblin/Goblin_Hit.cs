using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Hit : MonsterState<GoblinState>
{
    float stunTime;
    bool isTransition;
    public override void Enter()
    {
        base.Enter();
        anim.Play(AnimIdTable.GetInstance.Hit1Id);
        isTransition = false;
        stunTime = owner.StunTime;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
    }
    Coroutine coroutine = null;
    IEnumerator WaitCo()
    {
        owner.FlipCheck(pos.Velocity2D());
        yield return new WaitForSeconds(stunTime);
        isTransition = true;
    }
    public override void Exit()
    {
        base.Exit();
        if (coroutine != null)
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
