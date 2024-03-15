using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Hit : PlayerBaseState<PlayerState>
{
    float delay;
    bool isTransition;
    [SerializeField] AudioClip[] clips;
    public override void Enter()
    {
        //base.Enter();
        isTransition = false;

        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCoroutine());
        owner.OnStartAlert();
        int playIdx = UnityEngine.Random.Range(0, clips.Length);
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[playIdx] != null)
                Manager.Sound.PlayVoice(clips[playIdx]);
        }
        int idx = UnityEngine.Random.Range(0, 2);
        delay = owner.StunTime;
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
            owner.SetState = PlayerState.Idle;
    }
}
