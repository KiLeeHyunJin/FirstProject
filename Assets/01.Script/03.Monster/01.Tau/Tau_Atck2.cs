using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tau_Atck2 : MonsterState<TauState> //µ¹Áø
{
    bool isTransition;
    [SerializeField] float duringTime;
    [SerializeField] float atckSpeed;
    Vector3 AttackSize;
    Vector2 AttackOffset;
    Vector2 AttackPower;
    float[] AttackTime;
    Collider2D[] colliders = new Collider2D[1];
    float checkLength;
    int direction;
    public override void Enter()
    {
        AttackSize = owner.GetAtckData(1).AttackSize;
        AttackOffset = owner.GetAtckData(1).AttackOffset;
        AttackPower = owner.GetAtckData(1).AttackPower;
        int length = owner.GetAtckData(1).AttackTimming.Length;
        AttackTime = new float[length];

        for (int i = 0; i < length; i++)
            AttackTime[i] = duringTime * owner.GetAtckData(1).AttackTimming[i];

        checkLength = AttackSize.x > AttackSize.y ? AttackSize.x : AttackSize.y;
        if (pos.direction == TransformPos.Direction.Right)
            direction = 1;
        else
            direction = -1;
        isTransition = false;
        anim.Play(AnimIdTable.GetInstance.Atck2Id);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(Attack());
    }
    public override void Update()
    {
        pos.AddForceMove(Vector2.right * direction * atckSpeed);
    }
    Coroutine coroutine = null;
    IEnumerator Attack()
    {
        float time = 0;
        int i = 0;
        while (time < duringTime)
        {
            if(i < AttackTime.Length)
            {
                if (time >= AttackTime[i])
                {
                    Vector2 Offset = new Vector2(AttackOffset.x * direction, AttackOffset.y);
                    int targetCount = Physics2D.OverlapCircleNonAlloc(
                    new Vector2(pos.X, pos.Z + pos.Y) + Offset
                    , checkLength, colliders, owner.layerMask);
                    Debug.Log("Assult Check");
                    if (targetCount > 0)
                    {

                        IDamagable damagable = colliders[0].GetComponent<IDamagable>();
                        if (damagable != null)
                        {
                            damagable.IGetDamage(0, owner.GetAtckData(1).AttackEffect);
                            damagable.ISetKnockback(
                                new Vector2(AttackPower.x * direction, AttackPower.y),
                                pos.Pose,
                                AttackSize,
                                Offset,
                                owner.GetAtckData(1).AttackType,
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
        owner.ResetCoolTime(MonsterController<TauState>.AtckEnum.Atck2);
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = TauState.AtckFinish;
    }
}
