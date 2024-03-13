using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Atck3 : MonsterState<TauState> // �Ͽ︵
{
    bool isTransition;
    [SerializeField] float duringTime;
    Vector3 AttackSize;
    Vector2 AttackOffset;
    Vector2 AttackPower;
    float[] AttackTime;
    Collider2D[] colliders = new Collider2D[1];
    float checkLength;
    int direction;
    public override void Enter()
    {
        SetActckData();
        anim.Play(AnimIdTable.GetInstance.Atck3Id);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(Attack());

    }

    void SetActckData()
    {
        AttackSize = owner.GetAtckData(2).AttackSize;
        AttackOffset = owner.GetAtckData(2).AttackOffset;
        AttackPower = owner.GetAtckData(2).AttackPower;

        int length = owner.GetAtckData(2).AttackTimming.Length;
        AttackTime = new float[length];

        for (int i = 0; i < length; i++)
            AttackTime[i] = duringTime * owner.GetAtckData(2).AttackTimming[i];

        checkLength = AttackSize.x > AttackSize.y ? AttackPower.x : AttackPower.y;
        if (pos.direction == TransformPos.Direction.Right)
            direction = 1;
        else
            direction = -1;
        isTransition = false;
    }

    Coroutine coroutine = null;
    IEnumerator Attack()
    {
        float time = 0;
        int i = 0;
        while (time < duringTime)
        {
            if (i < AttackTime.Length)
            {
                if (time >= AttackTime[i])
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
                            //damagable.IGetDamage(value);
                            damagable.ISetKnockback(
                                new Vector2(AttackPower.x * dir, AttackPower.y),
                                pos.Pose,
                                AttackSize,
                                Offset,
                                owner.GetAtckData(1).stunTime,
                                 owner.GetAtckData(1).pushTime[i]
                                );
                        }
                    }
                    i++;
                }
            }
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        isTransition = true;

    }

    public override void FixedUpdate()
    {
        pos.Synchro();
    }

    public override void Exit()
    {
        owner.ResetCoolTime(MonsterController<TauState>.AtckEnum.Atck3);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = TauState.AtckFinish;
    }
}
