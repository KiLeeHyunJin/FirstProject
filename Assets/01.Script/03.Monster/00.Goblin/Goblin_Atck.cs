using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Atck : MonsterState<GoblinState>
{
    [SerializeField] GameObject prefab;
    [Range(0, 1)]
    [SerializeField] float attackTime;
    [SerializeField] float speed;
    bool isTransition;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.AtckId);
        isTransition = false;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(Attack());
    }
    public override void FixedUpdate()
    {
        pos.Synchro();
    }
    Coroutine coroutine = null;
    IEnumerator Attack()
    {
        while(true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackTime)
            {
                //int dir =  pos.direction == TransformPos.Direction.Right ? 1 : -1;
                //GameObject obj = GameObject.Instantiate(prefab);
               break;
            }
            yield return new WaitForFixedUpdate();
        }
        while(true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
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
        if (isTransition)
            owner.SetState = GoblinState.Idle;
    }
}
