using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public enum StandingState
{
    Stand, Fall, Down, Sit
}
public abstract class BaseController<T> : MonoBehaviour, IConnectController where T : Enum
{
    public float StunTime { get; private set; }
    public int MinusHp { set { StateHp -= value; } }
    public bool isDie { get; protected set; }
    [SerializeField] AudioClip dieClip;
    public StateMachine<T> fsm;
    [field : SerializeField]
    public T CurrentState 
    { 
        get;
        private set; 
    }
    public T NextState { get; protected set; }
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

     protected TransformPos transformPos;
    protected int StateHp { get; set; }
    [field: SerializeField] protected float StateMaxHp { get; set; }

    protected virtual void Awake()
    {
        StateHp = (int)StateMaxHp;
        isDie = false;
            transformPos = GetComponent<TransformPos>();
        fsm = new StateMachine<T>();
    }
    protected virtual void SetStateData(BaseState<T> state)
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
    }
    protected virtual void Die()
    {
        isDie = true;
        transformPos.AddForce(transformPos.Velocity() + (Vector3.up * 6));
        if (dieClip != null)
            Manager.Sound.PlaySFX(dieClip);
    }
 

    public virtual void ISetDamage(int damage, AttackEffectType effectType, float stunTime)
    {
        StunTime = stunTime;
        MinusHp = damage;
        if (isDie == false && StateHp <= 0)
           Die();
    }

    public virtual void ISetType()
    {
    }

    public StandingState IGetStandingType()
    {
        return StandState;
    }
}
