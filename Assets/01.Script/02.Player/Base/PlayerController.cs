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

    [Header("¸µÅ©")]
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Animator anim;
    [SerializeField] AttackController atkController;

    TransformPos transformPos;



    public AttackSkill currentSkill;
    [SerializeField] Walk walk;
    JumpSystem jump;

    NormalAttack attack;
    ShockDownAttack shockDownAttack;
    ShortAirSlash shortAirSlash;
    public void Awake()
    {
        fsm = new StateMachine<State>();
        jump = GetComponent<JumpSystem>();
        transformPos = GetComponent<TransformPos>();
        transformPos.direction = TransformPos.Direction.Right;
        walk.Setting(anim,transformPos, atkController,renderer);
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
        GetSkill();
        anim = GetComponentInChildren<Animator>();
        anim.speed = .35f;
    }
    private void Update()
    {
        fsm?.Update();
    }

    float keyDownTime;

    public void OnJump(InputValue inputValue)
    {
        jump.StartJump();
    }

    public void OnBaseAttack(InputValue inputValue)
    {
        attack.InputKeyCount();
    }
}
