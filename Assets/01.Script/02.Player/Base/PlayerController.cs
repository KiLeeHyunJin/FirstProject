using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public StateMachine<State> fsm;

    [Header("¸µÅ©")]
    [SerializeField] Transform yPos;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Animator anim;
    [SerializeField] AttackController atkController;
    Rigidbody2D rigid;
    TransformPos transformPos;

    public AttackState currentSkill;
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
        fsm.Start(State.Idle);
        GetSkill();
        anim = GetComponentInChildren<Animator>();
        anim.speed = .35f;
    }
    private void Update()
    {
        fsm?.Update();
        LockYObject();
        if(Input.GetKeyDown(KeyCode.S))
        {
            transformPos.AddForce(new Vector2(1, 0.5f));
            SetState = State.Hit;
        }
    }

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
