using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Jump, Action, Down, Hit, Stune, Idle, Alert, Walk , Run, Reclined, 
    }
    public State CurrentState;
    public StateMachine<State> fsm;

    [Header("수직 움직임 속도")]
    [SerializeField] float verticalWalkSpeed;
    [SerializeField] float verticalRunSpeed;

    [Header("수평 움직임 속도")]
    [SerializeField] float horizontalWalkSpeed;
    [SerializeField] float horizontalRunSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float horizontalSpeed;

    [Header("링크")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] new SpriteRenderer renderer;


    Vector3 moveValue;

    JumpSystem jump;
    Animator anim;
    NormalAttack attack;
    ShockDownAttack shockDownAttack;
    ShortAirSlash shortAirSlash;
    TransformPos transformPos;
    public AttackSkill currentSkill;
    Tester tester;
    Walk walk;
    public void Awake()
    {
        fsm = new StateMachine<State>();
        jump = GetComponent<JumpSystem>();
        transformPos = GetComponent<TransformPos>();
        transformPos.direction = TransformPos.Direction.Right;
        walk = new Walk();
        walk.SetStateMachine(fsm);
        fsm.AddState(State.Walk,walk);
        fsm.Start(State.Walk);
    }
    void GetSkill()
    {
        attack = GetComponent<NormalAttack>();
        shockDownAttack = GetComponent<ShockDownAttack>();
        shortAirSlash = GetComponent<ShortAirSlash>();
    }
    public void Start()
    {
        tester = new Tester(GetComponent<PlayerInput>());
        GetSkill();
        anim = GetComponentInChildren<Animator>();
        anim.speed = .35f;
    }
    private void Update()
    {
        fsm?.Update();
    }
    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

        if(moveValue.magnitude != 0)
        {
            Vector2 moveVector = new Vector2(moveValue.x * horizontalSpeed, moveValue.y * verticalSpeed);
            rigid.velocity = moveVector;
            TransformPos.Direction before = transformPos.direction;

            if(moveValue.x != 0)
            {
                if (moveValue.x < 0)
                    transformPos.direction = TransformPos.Direction.Left;
                else if (moveValue.x > 0)
                    transformPos.direction = TransformPos.Direction.Right;
            }

            if(before != transformPos.direction)
            {
                renderer.flipX = !renderer.flipX;
            }
        }
    }
    float keyDownTime;
    public void OnMove(InputValue inputValue)
    {
        if (CurrentState == State.Alert || CurrentState == State.Idle)
        { 
        }

        float beforeTime = keyDownTime;
        keyDownTime = Time.time;
        if (keyDownTime - beforeTime < 0.2f)
        {
            anim.Play("Run");
            horizontalSpeed = horizontalRunSpeed;
            verticalSpeed = verticalRunSpeed;
        }
        else
        {
            anim.Play("Walk");
            horizontalSpeed = horizontalWalkSpeed;
            verticalSpeed = verticalWalkSpeed;
        }

        Vector2 value = inputValue.Get<Vector2>().normalized;
        moveValue = new Vector3(value.x, value.y, 0);

        if (moveValue.sqrMagnitude == 0)
        {
            rigid.velocity = Vector2.zero;
            anim.Play("Idle");
        }
    }
    public void OnJump(InputValue inputValue)
    {
        Debug.Log("본캐 발동!");
        jump.StartJump();
    }

    public void OnBaseAttack(InputValue inputValue)
    {
        attack.InputKeyCount();
    }
}
public class Tester
{
    PlayerInput PlayerInput;
    public Tester(PlayerInput input)
    {
        PlayerInput = input;
    }
    public void OnJump(InputValue value)
    {
        Debug.Log("부캐 발동!");
    }
}
