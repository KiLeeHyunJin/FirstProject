using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackState;
public abstract class SkillStateController : PlayerBaseState<PlayerState>
{
    [HideInInspector] public KeyManager.QuickKey currentSkillKey;
    [field: SerializeField] public ProjectileObj projectile { get; private set; }
    [field :SerializeField] public float objectSpeed { get; private set; }
    [field : SerializeField] public AttackType AttackType { get; private set;}
    [SerializeField] private bool hasCoolTime;
    [SerializeField] protected float coolTime;
    [SerializeField] protected AttackData[] attackData;
    [field: SerializeField] public bool On { get; private set; }
    [HideInInspector] public bool Click;
    protected bool isTransition;

    SkillState[] skillStates;
    public SkillState GetState
    {
        get
        {
            if (skillStates.Length <= inputCount)
                return null;
            return skillStates[inputCount];
        }
    }
    public int inputCount { get; set; }
    public bool isCool { get; private set; }
    bool returnState;

    public void SetSkillData(SkillState state, int idx)
    {
        if (state == null) 
            return;
        if (attackData.Length <= idx)
            return;
        
        if (skillStates == null)
        {
            skillStates = new SkillState[attackData.Length];
        }

        state.SetSkillData(this, attackData[idx], Animator.StringToHash(attackData[idx].AnimName), attackData[idx].chainAnim);
        skillStates[idx] = state;
    }

    public override void Enter()
    {
        On = true;
        inputCount = 0;
        returnState = false;
        isTransition = false;
        if (ReadyCheck() == false)
            returnState = true;
        else 
            isTransition = true;
    }
    public override void Transition()
    {
        if (isTransition)
            EnterState();
        else if (returnState)
        {
            Out();
            owner.SetState = PlayerState.Idle;
        }
    }

    protected abstract void EnterState();

    public void Out()
    {
        On = false;
        Click = false;
        owner.activeType = ActiveType.Normal;
        if (hasCoolTime == false)
            return;
        if (coroutine != null)
            owner.StopCoroutine(coroutine);
        coroutine = owner.StartCoroutine(CoolDown());
    }
    Coroutine coroutine = null;
    IEnumerator CoolDown()
    {
        isCool = true;
        yield return new WaitForSeconds(coolTime);
        isCool = false;
    }

    bool ReadyCheck()
    {
        if (owner.Mp >= attackData[0].mana && 
            isCool == false)
        {
            owner.MinusMp = attackData[0].mana;
            return true;
        }
        return false;
    }
}
