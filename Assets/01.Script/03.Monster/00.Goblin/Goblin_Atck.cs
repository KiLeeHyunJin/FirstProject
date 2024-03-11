using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Atck : MonsterState<GoblinState>
{
    [SerializeField] AttackObj prefab;
    float attackTime;
    int dir;
    bool isTransition;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.AtckId);
        isTransition = false;
        owner.ResetCoolTime(MonsterController<GoblinState>.AtckEnum.Atck1);
        attackTime = owner.GetAtckData(0).AttackTimming[0];
        dir = pos.direction == TransformPos.Direction.Right ? 1 : -1;
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
                if (prefab == null)
                    break;
                AttackObj obj = GameObject.Instantiate(prefab);
                obj.transform.position = new Vector2(pos.X, pos.Z);
                obj.SetData(new Vector2(1, 1), pos.Pose, dir);
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        while(true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
                break;
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
