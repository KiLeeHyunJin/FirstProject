using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class Tau_Atck1 : MonsterState<TauState> //³»·Á Âï±â
{
    bool isTransition;
    [SerializeField]
    Vector3 AttackSize;
    [SerializeField]
    Vector2 AttackOffset;
    [SerializeField]
    Vector2 AttackPower;
    [Range(0, 1)]
    [SerializeField]
    float AttackTime;
    [SerializeField]
    LayerMask layerMask;
    Collider2D[] colliders = new Collider2D[1];
    float checkLength;
    int direction;
    public override void Enter()
    {
        checkLength = AttackSize.x > AttackSize.y ? AttackPower.x : AttackPower.y;
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
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= AttackTime)
            {
                Vector2 Offset = new Vector2(AttackOffset.x * direction, AttackOffset.y);
                int targetCount = Physics2D.OverlapCircleNonAlloc(
           new Vector2(pos.X, pos.Z + pos.Y) + Offset
           , checkLength, colliders, layerMask);
                
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
                            Offset
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
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
    }

    public override void Transition()
    {
        if (isTransition)
            owner.SetState = TauState.Idle;
    }

}
