using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AttackState;

public abstract class SkillStateController : PlayerBaseState<PlayerState>
{
    [HideInInspector] public KeyManager.Key currentSkillKey;

    [SerializeField] protected float coolTime;
    [SerializeField] protected AttackData[] attackData;
    [SerializeField] bool repeatAnim;
    [SerializeField] bool hasCoolTime;

    protected bool isTransition;

    public SkillState[] skillStates { get; private set; }
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
            skillStates = new SkillState[attackData.Length];

        state.skillController = this;
        state.attackData = attackData[idx];
        state.animId = Animator.StringToHash(attackData[idx].AnimName);
        state.chaingAnim = attackData[idx].chaingAnim;
        skillStates[idx] = state;
    }

    public override void Enter()
    {
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
            owner.SetState = PlayerState.Idle;
    }
    protected abstract void EnterState();
    public void FinishSkill()
    {
        isCool = true;
        coroutine = owner.StartCoroutine(CoolDown());
    }
    Coroutine coroutine = null;
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolTime);
        isCool = false;
    }
    bool ReadyCheck()
    {
        if (owner.Mp >= attackData[0].mana && 
            isCool == false)
            return true;
        return false;
    }
}
