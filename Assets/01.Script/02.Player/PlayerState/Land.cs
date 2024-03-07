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
    bool isTransition;
    public override void Enter()
    {
        isTransition = false;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(DelayCo());
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
            owner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
        {
            if(owner.WalkType == PlayerController.State.Run)
                owner.SetState = PlayerController.State.Run;
            else if(owner.WalkType == PlayerController.State.Walk)
                owner.SetState = PlayerController.State.Walk;
            else
                owner.SetState = PlayerController.State.Idle;

        }
    }
}
