using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StandingState
{
    Stand, Fall, Down, Sit
}
public abstract class BaseController<T> : MonoBehaviour, IConnectController where T : Enum
{

    public StateMachine<T> fsm;
    [field : SerializeField]
    public T CurrentState 
    { 
        get;
        private set; 
    }
    public T SetState 
    { 
        set 
        { 
            CurrentState = value; 
            fsm.ChangeState(value); 
        } 
    }
    public StandingState StandState 
    { 
        get; 
        private set; 
    }
    public StandingState SetStandState 
    { 
        set 
        { 
            StandState = value; 
        } 
    }

    [SerializeField] protected new SpriteRenderer renderer;
    [SerializeField] protected Animator anim;

    [SerializeField] protected TransformPos transformPos;

    protected virtual void Awake()
    {
        if(transformPos == null)
            transformPos = GetComponent<TransformPos>();
        fsm = new StateMachine<T>();
    }
    void SetStateData(BaseState<T> state)
    {
        state.SetStateMachine(fsm);
        state.Setting(anim, transformPos, renderer);
    }
    protected virtual void Start()
    {

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        fsm.Update();
    }
    protected virtual void FixedUpdate()
    {
        fsm.FixedUpdate();
    }

    public void FlipCheck(Vector2 moveValue)
    {
        if (moveValue.x == 0)
            return;
        TransformPos.Direction before = transformPos.direction;
        if (moveValue.x < 0)
            transformPos.SetDirection = TransformPos.Direction.Left;
        else
            transformPos.SetDirection = TransformPos.Direction.Right;
        //if (before != transformPos.direction)
        //    renderer.flipX = !renderer.flipX;
    }

    public virtual void ISetDamage(float damage, AttackEffectType effectType)
    {

    }

    public virtual void ISetType()
    {
    }

    public StandingState IGetStandingType()
    {
        return StandState;
    }
}
