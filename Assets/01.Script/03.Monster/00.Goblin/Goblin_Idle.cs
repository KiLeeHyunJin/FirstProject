using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Idle : MonsterState<GoblinState>
{
    [SerializeField] Vector2 duringTime;
    float idleTime;
    bool isTransition;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.IdleId);
        isTransition = false;
        idleTime = UnityEngine.Random.Range(duringTime.x, duringTime.y);
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(waitCo());
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    Coroutine coroutine = null;
    IEnumerator waitCo()
    {
        yield return new WaitForSeconds(idleTime);
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
            owner.SetState = GoblinState.Walk;
    }
}
