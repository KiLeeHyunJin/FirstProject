using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Fall : PlayerBaseState<PlayerState>
{
    bool isTransition;

    public override void Enter()
    {
        isTransition = false;
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        owner.OnStartAlert();
        coroutine = owner.StartCoroutine(FallingCo());
        anim.Play(AnimIdTable.GetInstance.FallingId);
        owner.SetStandState = StandingState.Fall;
    }
    public override void Exit()
    {
        pos.ForceZero(KeyCode.X);
    }
    public override void FixedUpdate()
    {
        pos.JumpingFreezePosition();
    }
    Coroutine coroutine = null;
    IEnumerator FallingCo()
    {
        while (pos.Velocity().y > 0)
            yield return new WaitForFixedUpdate();

        while (pos.Y > 0 || pos.yState() != TransformAddForce.YState.None)
            yield return new WaitForFixedUpdate();
        isTransition = true;
    }
    public override void Transition()
    {
        if(isTransition)
            owner.SetState = PlayerState.Down;
    }
}
