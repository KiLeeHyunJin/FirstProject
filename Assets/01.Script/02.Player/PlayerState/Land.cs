using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Land : PlayerBaseState<PlayerState>
{
    [Range(0,0.5f)]
    [SerializeField]
    float delayTime;
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        if (coroutine != null)
            playerOwner.StopCoroutine(coroutine);
        coroutine = playerOwner.StartCoroutine(DelayCo());
        pos.ForceZero();
    }
    Coroutine coroutine = null;
    IEnumerator DelayCo()
    {
        anim.Play("Jump_Land");
        yield return new WaitForSeconds(delayTime);
        isTransition = true;
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    public override void Exit()
    {
        if (coroutine != null)
            playerOwner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
        {
            if(playerOwner.WalkType == PlayerState.Run)
                playerOwner.SetState = PlayerState.Run;
            else if(playerOwner.WalkType == PlayerState.Walk)
                playerOwner.SetState = PlayerState.Walk;
            else
                playerOwner.SetState = PlayerState.Idle;

        }
    }
}
