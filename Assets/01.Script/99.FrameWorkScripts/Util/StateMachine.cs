using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine<T> where T : Enum
{
    private Dictionary<T, BaseState<T>> stateDic = new Dictionary<T, BaseState<T>>();
    private BaseState<T> curState;
    
    //public bool IsValueType(T state)
    //{
    //    if(stateDic.ContainsKey(state))
    //        return stateDic[state].ValueType;
    //    return false;
    //}
    public void Start(T startState)
    {
        curState = stateDic[startState];
        curState.Enter();
    }

    public void Update()
    {
        if (curState == null)
            return;
        curState.Update();
        curState.Transition();
    }

    public void LateUpdate() => curState?.LateUpdate();

    public void FixedUpdate() => curState?.FixedUpdate();

    public void AddState(T stateEnum, BaseState<T> state)
    {
        state.SetStateMachine(this);
        stateDic.Add(stateEnum, state);
    }

    public void ChangeState(T stateEnum)
    {
        curState.Exit();
        curState = stateDic[stateEnum];
        curState.Enter();
    }
}
[Serializable]
public abstract class BaseState<T> where T : Enum
{
    private StateMachine<T> stateMachine;
    protected Animator anim;
    protected TransformPos pos;
    protected SpriteRenderer renderer;
    protected BaseController<T> owner;
    public void Setting(Animator _anim, TransformPos _transform, SpriteRenderer _renderer, BaseController<T> _owner)
    {
        anim = _anim;
        pos = _transform;
        renderer = _renderer;
        owner = _owner;
    }
    public void SetStateMachine(StateMachine<T> stateMachine) => this.stateMachine = stateMachine;

    protected void ChangeState(T stateEnum) => stateMachine.ChangeState(stateEnum);

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }


    public virtual void LateUpdate() { }
    public virtual void FixedUpdate() { }

    public abstract void Transition();
}

[Serializable]
public abstract class PlayerBaseState<T> : BaseState<T> where T : Enum
{
    //protected PlayerController owner;
    protected AttackController attack;
    protected PlayerController playerOwner;
    public void SettingAttackData(AttackController _atckCon)
    {
        attack = _atckCon;
        PlayerController player = owner as PlayerController;
        if (player != null)
        {
            playerOwner = player;
        }
    }
    //public void Setting(PlayerController _controller, Animator _anim, TransformPos _transform, AttackController _atckCon, SpriteRenderer _renderer)
    //{
    //    anim = _anim;
    //    pos = _transform;
    //    attack = _atckCon;
    //    renderer = _renderer;
    //    owner = _controller;
    //}
}