using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMachine<T> where T : Enum
{
    private Dictionary<T, BaseState<T>> stateDic = new Dictionary<T, BaseState<T>>();
    private BaseState<T> curState;

    public Action<InputAction.CallbackContext> GetEnterMethod(T state)
    {
        if (stateDic.ContainsKey(state))
            return stateDic[state].StartedInputAction;
        return null;
    }
    public Action<InputAction.CallbackContext> GetEventMethod(T state)
    {
        if (stateDic.ContainsKey(state))
            return stateDic[state].PerformedInputAction;
        return null;
    }
    public Action<InputAction.CallbackContext> GetExitMethod(T state)
    {
        if (stateDic.ContainsKey(state))
            return stateDic[state].CanceledInputAction;
        return null;
    }
    public void Start(T startState)
    {
        curState = stateDic[startState];
        curState.Enter();
    }

    public void Update()
    {
        curState.Update();
        curState.Transition();
    }

    private void LateUpdate()
    {
        curState.LateUpdate();
    }

    private void FixedUpdate()
    {
        curState.FixedUpdate();
    }

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
public class BaseState<T> where T : Enum
{
    private StateMachine<T> stateMachine;
    protected Animator anim;
    protected TransformPos pos;
    protected AttackController attack;
    protected SpriteRenderer renderer;
    public BaseState(Animator _anim, TransformPos _transform, AttackController _atckCon, SpriteRenderer _renderer)
    {

    }
    public void Setting(Animator _anim, TransformPos _transform, AttackController _atckCon, SpriteRenderer _renderer)
    {
        anim = _anim;
        pos = _transform;
        attack = _atckCon;
        renderer = _renderer;
    }
    public void SetStateMachine(StateMachine<T> stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void ChangeState(T stateEnum)
    {
        stateMachine.ChangeState(stateEnum);
    }

    public virtual void StartedInputAction(InputAction.CallbackContext context) { }
    public virtual void CanceledInputAction(InputAction.CallbackContext context) { }
    public virtual void PerformedInputAction(InputAction.CallbackContext context) { }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }


    public virtual void LateUpdate() { }
    public virtual void FixedUpdate() { }

    public virtual void Transition() { }
}