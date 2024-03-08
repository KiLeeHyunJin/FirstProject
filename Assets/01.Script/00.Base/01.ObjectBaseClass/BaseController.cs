using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController<T> : MonoBehaviour where T : Enum
{
    public StateMachine<T> fsm;
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

    [SerializeField] protected new SpriteRenderer renderer;
    [SerializeField] protected Animator anim;

    protected TransformPos transformPos;

    protected virtual void Awake()
    {
        transformPos = GetComponent<TransformPos>();
        fsm = new StateMachine<T>();
    }
    void SetStateData(BaseState<T> state)
    {
        state.SetStateMachine(fsm);
        state.Setting(anim, transformPos, renderer,this);
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
            transformPos.direction = TransformPos.Direction.Left;
        else
            transformPos.direction = TransformPos.Direction.Right;

        if (before != transformPos.direction)
            renderer.flipX = !renderer.flipX;
    }
}
