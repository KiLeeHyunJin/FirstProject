using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_AtckFinish : MonsterState<TauState>
{ 
    bool isTransition;
    [SerializeField]
    float waitingTime;
    public override void Enter()
    {
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.AtckFinishId);
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(WaitCo());
    }
    Coroutine coroutine = null;

    IEnumerator WaitCo()
    {
        yield return new WaitForSeconds(waitingTime);
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
        {
            owner.ResetCoolTime(MonsterController<TauState>.AtckEnum.Atck2);
            owner.SetState = TauState.Idle;
        }
    }

}
