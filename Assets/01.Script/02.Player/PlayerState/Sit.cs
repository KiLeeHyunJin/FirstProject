using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sit : PlayerBaseState<PlayerState>
{
    [SerializeField] float readyTime;
    [SerializeField] float defaultTime;
    bool isTransition;
    bool isReady;
    public override void Enter()
    {
        isTransition = false;
        isReady = false;
        if(coroutine != null)
            playerOwner.StopCoroutine(coroutine);
        coroutine = playerOwner.StartCoroutine(WaitCo());
        anim.Play(AnimIdTable.GetInstance.SitId);
    }
    Coroutine coroutine = null;
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(defaultTime);
        isReady = true;
        yield return new WaitForSeconds(readyTime - defaultTime);
        isTransition = true;
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
            playerOwner.SetState = PlayerState.Idle;
        }
        else if (isReady)
        {
            if (
                playerOwner.keys.ContainLayer(KeyManager.Key.Move))
                playerOwner.SetState = PlayerState.Walk;
            else if (
                playerOwner.keys.ContainLayer(KeyManager.Key.C))
                playerOwner.SetState = PlayerState.JumpUp;
            else if (
                playerOwner.keys.ContainLayer(KeyManager.Key.X))
                playerOwner.SetState = PlayerState.Interaction;
        }

    }
}
