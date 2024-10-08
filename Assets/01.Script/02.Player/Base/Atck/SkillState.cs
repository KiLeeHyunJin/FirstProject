using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
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
    bool isPlaySound;
    protected int direction;

    Collider2D[] colliders;
    public void SetSkillData(SkillStateController _skillController, AttackData _attackData, int _animId, bool _chaingAnim)
    {
        skillController = _skillController;
        attackData = _attackData;
        animId = _animId;
        chaingAnim = _chaingAnim;
        colliders = new Collider2D[10];
        isPlaySound = true;
    }

    public override void Enter()
    {
        //base.Enter();
        if(attackData.mana > 0)
        {
            if (owner.Mp >= attackData.mana)
            {
                owner.MinusMp = attackData.mana;
            }
            else
            {
                isTransition = true;
                return;
            }
        }

        SetEnter();
        direction = pos.direction == TransformPos.Direction.Left ? -1 : 1;
        owner.activeType = ActiveType.Skill;
        attackIdx = 0;
        moveIdx = 0;
        skillController.inputCount++;
        anim.Play(animId);
        nextAnim = false;
        isTransition = false;
        EnterAction();
        if(isPlaySound)
        {
            attack.SetSoundClip(attackData.soundSwingClip, attackData.soundHitClip);

            if(attackData.soundClip != null)
            {
                if (soundRoutine != null)
                    owner.StopCoroutine(soundRoutine);
                soundRoutine = owner.StartCoroutine(PlayVoice());
            }

            if(attackData.skillEffectClip != null)
            {
                if (effectSoundRoutine != null)
                    owner.StopCoroutine(effectSoundRoutine);
                effectSoundRoutine = owner.StartCoroutine(EffectSound());
            }

            isPlaySound = false;
        }
    }
    Coroutine effectSoundRoutine = null;
    IEnumerator EffectSound()
    {
        yield return new WaitForSeconds(attackData.skillEffectPlayTime);
        Manager.Sound.PlaySFX(attackData.skillEffectClip);
    }

    Coroutine soundRoutine = null;
    IEnumerator PlayVoice()
    {
        yield return new WaitForSeconds(attackData.soundPlayeTime);
        Manager.Sound.PlayVoice(attackData.soundClip);
    }
    protected virtual void SetEnter()
    { }

    public override void Exit()
    {
        base.Exit();
        isPlaySound = true;
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
            if (attackData.attackCounts[attackIdx].AttackEffectSoundClip != null)
                Manager.Sound.PlayVoice(attackData.attackCounts[attackIdx].AttackEffectSoundClip);

            attack.SetDamage(attackData.attackCounts[attackIdx].Percent, attackData.damage);

            attack.SetPosition(
                new Vector2(
                    attackData.attackSize.offset.x * direction,
                    attackData.attackSize.offset.y),
                    attackData.attackSize.size,
                    attackData.attackCounts[attackIdx].gather) ;



            attack.SetKnockBack(
                new Vector2(
                    attackData.attackCounts[attackIdx].power.x * direction,
                    attackData.attackCounts[attackIdx].power.y),
                    attackData.attackCounts[attackIdx].effectType,
                    attackData.attackCounts[attackIdx].stunTime,
                    attackData.attackCounts[attackIdx].pushTime
                );

            attack.OnAttackEnable();
            attackIdx++;
        }
    }

    private void CheckTransition(float normalTime)
    {
        if (attackData.charging)
            return;
        if(chaingAnim == false)
        {
            if(owner.activeType == ActiveType.Skill)
            {
                if (normalTime >= attackData.delay)
                    owner.activeType = ActiveType.Normal;
                else
                    return;
            }
            else
            {
                if (skillController.Click)
                {
                    skillController.inputCount++;
                    nextAnim = true;
                }
                if (normalTime >= 1)
                    isTransition = true;
            }
        }
        else
        {
            if (normalTime >= 1)
            {
                skillController.inputCount++;
                nextAnim = true;
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
