using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Jump, Action, Down, Hit, Stune, Idle, Alert, Walk, Run, Reclined, Land, BasicAtck, JumpAtck,
    }
    [field: SerializeField] public State CurrentState { get; private set; }
    public State SetState
    {
        set
        {
            CurrentState = value;
            fsm.ChangeState(CurrentState);
        }
    }
    public AnimIdTable animId { get; private set; }
    public StateMachine<State> fsm;
    [Header("¸µÅ©")]
    [SerializeField] Transform yPos;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Animator anim;
    [SerializeField] AttackController atkController;
    Rigidbody2D rigid;
    TransformPos transformPos;
    Vector2 moveValue;

    public AttackState currentSkill;
    [SerializeField] Mover mover;
    [SerializeField] Walk walk;
    [SerializeField] Jump jump;
    [SerializeField] Idle idle;
    [SerializeField] Land land;
    [SerializeField] Hit hit;
    [SerializeField] Down down;
    NormalAttack attack;
    ShockDownAttack shockDownAttack;
    ShortAirSlash shortAirSlash;


    public void Awake()
    {
        fsm = new StateMachine<State>();
        transformPos = GetComponent<TransformPos>();
        rigid = GetComponent<Rigidbody2D>();

        //mover.Setting(transformPos, this, renderer);
        SetStateData(walk);
        SetStateData(jump);
        SetStateData(idle);
        SetStateData(land);
        SetStateData(hit);
        SetStateData(down);
        

        fsm.AddState(State.Jump, jump);
        fsm.AddState(State.Walk, walk);
        fsm.AddState(State.Idle, idle);
        fsm.AddState(State.Land, land);
        fsm.AddState(State.Hit, hit);
        fsm.AddState(State.Down, down);

    }

    void SetStateData(BaseState<PlayerController.State> state)
    {
        state.Setting(this, anim, transformPos, atkController, renderer);
        state.SetStateMachine(fsm);
        transformPos.direction = TransformPos.Direction.Right;
    }

    void GetSkill()
    {
        attack = GetComponent<NormalAttack>();
        shockDownAttack = GetComponent<ShockDownAttack>();
        shortAirSlash = GetComponent<ShortAirSlash>();
    }

    public void Start()
    {
        GetSkill();
        anim = GetComponentInChildren<Animator>();
        fsm.Start(State.Idle);
    }
    private void Update()
    {
        //mover.Move(moveValue);
        fsm?.Update();
        LockYObject();
        if(Input.GetKeyDown(KeyCode.S))
        {
            transformPos.AddForce(new Vector2(1, 0.5f));
            SetState = State.Hit;
        }
    }

    //float inTime;
    //void OnMove(InputValue value)
    //{
    //    moveValue = value.Get<Vector2>();
    //    if (value.isPressed)
    //    {
    //        float beforeTime = inTime;
    //        inTime = Time.time;
    //        if (inTime - beforeTime < 0.2f)
    //            mover.Run();
    //        else
    //            mover.Walk();
    //    }
    //}

    void LockYObject()
    {
        if (transformPos.yState() != TransformAddForce.YState.None)
            yPos.localPosition = new Vector3(0, transformPos.Y, 0);
        else
            yPos.localPosition = Vector3.zero;
    }

    public void OnBaseAttack(InputValue inputValue)
    {
        //attack.InputKeyCount();
    }
}
