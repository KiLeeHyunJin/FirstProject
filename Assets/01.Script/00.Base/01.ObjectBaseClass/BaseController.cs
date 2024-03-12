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

    public int MinusHp { set { Hp -= value; } }

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
    [field: SerializeField] public int Hp { get; protected set; }

    protected virtual void Awake()
    {
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
        if (Hp <= 0)
        {
            if (coroutine == null)
                Die();
            return;
        }
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
    Coroutine coroutine = null;
    void Die()
    {
        coroutine = StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
        Color color = renderer.color;
        float alphaValue = color.a;
        while (color.a > 0)
        {
            alphaValue -= Time.deltaTime * 2;
            color = new Color(color.r, color.g, color.b, alphaValue);
            renderer.color = color;
            yield return new WaitForFixedUpdate();
        }
    }

    public virtual void ISetDamage(int damage, AttackEffectType effectType)
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
