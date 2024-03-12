using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackState;
public abstract class SkillState : PlayerBaseState<PlayerState>
{
    protected SkillStateController skillController;
    protected AttackData attackData;
    protected int animId;
    protected bool isTransition;
    protected bool chaingAnim;
    protected bool nextAnim;
    int moveIdx;
    int attackIdx;
    protected int direction;

    Collider2D[] colliders;
    public void SetSkillData(SkillStateController _skillController, AttackData _attackData, int _animId, bool _chaingAnim)
    {
        skillController = _skillController;
        attackData = _attackData;
        animId = _animId;
        chaingAnim = _chaingAnim;
        colliders = new Collider2D[10];
    }

    public override void Enter()
    {
        SetEnter();
        direction = pos.direction == TransformPos.Direction.Left ? -1 : 1;
        attackIdx = 0;
        moveIdx = 0;
        skillController.inputCount++;
        anim.Play(animId);
        nextAnim = false;
        isTransition = false;
        EnterAction();

    }
    protected virtual void SetEnter()
    { }

    public override void Exit()
    {
        base.Exit();
        ExitAction();
    }
    protected abstract void EnterAction();
    protected abstract void ExitAction();
    public override void Update()
    {
        float normalTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        MoveTiming(normalTime);
        AttackTiming(normalTime);
        Attack(normalTime);
        CheckTransition(normalTime);
    }
    private void MoveTiming(float normalTime)
    {
        if (moveIdx >= attackData.move.Length)
            return;

        if(normalTime >= attackData.move[moveIdx].moveTime)
        {
            Vector2 movePos = attackData.move[moveIdx].move;
            movePos.x *= direction;
            pos.AddForce(movePos, attackData.move[moveIdx].movingTime);
            moveIdx++;
        }
    }

    private void AttackTiming(float normalTime)
    {
        if (attackIdx >= attackData.attackCounts.Length)
            return;

        if (normalTime >= attackData.attackCounts[attackIdx].AttackTime)
        {
            attack.SetPosition(
                new Vector2(
                    attackData.attackSize.offset.x * direction,
                    attackData.attackSize.offset.y),
                attackData.attackSize.size,
                attackData.attackCounts[attackIdx].gather);

            attack.SetKnockBack(
                new Vector2(
                    attackData.attackCounts[attackIdx].power.x * direction,
                    attackData.attackCounts[attackIdx].power.y),
                    attackData.attackCounts[attackIdx].effectType,
                    attackData.attackCounts[attackIdx].pushTime
                );

            attack.SetDamage(attackData.attackCounts[attackIdx].Percent, attackData.damage);
            attack.OnAttackEnable();
            attackIdx++;
        }
    }

    private void CheckTransition(float normalTime)
    {
        if (skillController.Click) //키가 눌렸다.
        {
            if (normalTime >= attackData.delay) //제한 시간 지나고 눌렸다.
            {
                skillController.inputCount++;
                nextAnim = true;
            }
            skillController.Click = false;
        }
        else if (normalTime >= 1)
        {
            if (attackData.charging)
                return;
            if (chaingAnim)
            {
                skillController.inputCount++;
                nextAnim = true;
            }
            else
            {
                isTransition = true;
            }
        }
       
    }
    /// <summary>
    /// Update
    /// </summary>
    protected abstract void Attack(float normalTime);
    public override void Transition()
    {
        if(isTransition)
        {
            skillController.Out();
            owner.SetState = PlayerState.Idle;
        }
        else if(nextAnim)
        {
            owner.SetState = NextAnim();
        }
    }
    protected abstract PlayerState NextAnim();
    public void EmergencyEscape()
    {
        skillController.Out();
    }
}
