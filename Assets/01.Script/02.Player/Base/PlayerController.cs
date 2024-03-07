using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static KeyManager;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle, Jump, Alert, 
        Walk, Run, 
        Land,
        Hit, Stune, Down, Fall, Sit,
        Action, BasicAtck, JumpAtck, 
        Interaction
    }
    [field: SerializeField] public State CurrentState { get; private set; }
    public State SetState
    {
        set
        {
            CurrentState = value;
            if (value != State.Fall)
                fsm.ChangeState(CurrentState);
            else
                fsm.Start(CurrentState);
        }
    }
    public AnimIdTable animId { get; private set; }
    public StateMachine<State> fsm;
    [Header("¸µÅ©")]
    [SerializeField] Transform yPos;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Animator anim;
    [SerializeField] AttackController atkController;
    [SerializeField] public KeyManager keys { get; private set; }
    Rigidbody2D rigid;
    TransformPos transformPos;
    [field : SerializeField] public Vector2 moveValue { get; private set; }

    public AttackState currentSkill;
    [SerializeField] Idle idle;

    [SerializeField] Walk walk;
    [SerializeField] Run run;

    [SerializeField] Jump jump;
    [SerializeField] Land land;

    [SerializeField] Fall fall;
    [SerializeField] Hit hit;
    [SerializeField] Down down;
    [SerializeField] Sit sit;
    [SerializeField] InteracterKey interact;

    NormalAttack attack;
    ShockDownAttack shockDownAttack;
    ShortAirSlash shortAirSlash;


    public void Awake()
    {
        fsm = new StateMachine<State>();
        transformPos = GetComponent<TransformPos>();
        rigid = GetComponent<Rigidbody2D>();
        keys = GetComponent<KeyManager>();

        SetStateData(walk);
        SetStateData(run);
        SetStateData(jump);
        SetStateData(idle);
        SetStateData(land);
        SetStateData(hit);
        SetStateData(down);
        SetStateData(fall);
        SetStateData(sit);
        SetStateData(interact);
        

        fsm.AddState(State.Jump, jump);
        fsm.AddState(State.Interaction, interact);
        fsm.AddState(State.Walk, walk);
        fsm.AddState(State.Run, run);
        fsm.AddState(State.Idle, idle);
        fsm.AddState(State.Land, land);
        fsm.AddState(State.Hit, hit);
        fsm.AddState(State.Fall, fall);
        fsm.AddState(State.Down, down);
        fsm.AddState(State.Sit, sit);

    }
    public void Start()
    {
        GetSkill();
        anim = GetComponentInChildren<Animator>();
        fsm.Start(State.Idle);
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

    private void Update()
    {
        fsm?.Update();
        keys.ResetLayer();
    }

    private void FixedUpdate() => fsm?.FixedUpdate();

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
        if (moveValue.x == 0 && moveValue.y == 0)
            keys.OffMoveLayer();
        else
            keys.OnMoveLayer();
    }

    public void FlipCheck()
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
    public void CallDown()
    {
        SetState = State.Fall;
    }
}
