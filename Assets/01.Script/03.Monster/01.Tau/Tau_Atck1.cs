using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]

public class Tau_Atck1 : MonsterState<TauState> //³»·Á Âï±â
{
    bool isTransition;
    Vector3 AttackSize;
    Vector2 AttackOffset;
    Vector2 AttackPower;
    float AttackTime;
    Collider2D[] colliders = new Collider2D[1];
    float checkLength;
    int direction;
    public override void Enter()
    {
        AttackSize = owner.GetAtckData(0).AttackSize;
        AttackOffset = owner.GetAtckData(0).AttackOffset;
        AttackPower = owner.GetAtckData(0).AttackPower;
        AttackTime = owner.GetAtckData(0).AttackTimming[0];
        checkLength = AttackSize.x > AttackSize.y ? AttackSize.x : AttackSize.y;
        if (pos.direction == TransformPos.Direction.Right)
            direction = 1;
        else
            direction = -1;
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.Atck1Id);
        if(coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(Attack());
    }

    public override void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            isTransition = true;
    }
    Coroutine coroutine = null;
    IEnumerator Attack()
    {
        while(true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= AttackTime)
            {
                Vector2 Offset = new Vector2(AttackOffset.x * direction, AttackOffset.y);
                int targetCount = Physics2D.OverlapCircleNonAlloc(
                new Vector2(pos.X, pos.Z + pos.Y) + Offset
                , checkLength, colliders, owner.layerMask);

                if (targetCount > 0)
                {
                    int dir = colliders[0].transform.position.x - (pos.X + Offset.x) > 0 ? 1 : -1;

                    IDamagable damagable = colliders[0].GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        damagable.IGetDamage(0, owner.GetAtckData(0).AttackEffect);
                        damagable.ISetKnockback(
                            new Vector2(AttackPower.x * dir, AttackPower.y),
                            pos.Pose,
                            AttackSize,
                            Offset,
                            owner.GetAtckData(0).pushTime[0]
                            );
                    }
                }
                break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

    public override void FixedUpdate()
    {
        pos.Synchro();
    }

    public override void Exit()
    {
        owner.ResetCoolTime(MonsterController<TauState>.AtckEnum.Atck1);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = TauState.Idle;
    }

}
