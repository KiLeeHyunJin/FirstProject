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
        Idle, 
        Walk, Run, 
        JumpUp, JumpDown,Land,
        Hit, Stune, 
        Fall, Down, Sit,
        
        JumpAtck, Stap, LandAttack,
        Interaction, BasicAtck, ItemPick,

        Action,
    }
    [field: SerializeField] public State CurrentState { get; private set; }
    [SerializeField] float alertTime;

    public State SetState
    {
        set
        {
            CurrentState = value;
            fsm.ChangeState(value);
            //Debug.Log($"{value.ToString()}");
        }
    }
    public State WalkType { get; set; }
    public AnimIdTable animId { get; private set; }
    public StateMachine<State> fsm;
    [Header("¸µÅ©")]
    [SerializeField] Transform yPos;
    [SerializeField] new SpriteRenderer renderer;
    [SerializeField] Animator anim;
    [SerializeField] AttackController atkController;

    [SerializeField] public KeyManager keys { get; private set; }
    public bool isAlert { get; private set; }
    TransformPos transformPos;
    [field : SerializeField] public Vector2 moveValue { get; private set; }

    public AttackState currentSkill;
    [SerializeField] Idle idle;
    [SerializeField] Walk walk;
    [SerializeField] Run run;

    [SerializeField] JumpUp jumpUp;
    [SerializeField] JumpDown jumpDown;
    [SerializeField] Land land;

    [SerializeField] Fall fall;
    [SerializeField] Hit hit;
    [SerializeField] Down down;
    [SerializeField] Sit sit;
    [SerializeField] ItemPick itemPick;
    [SerializeField] InteracterKey interact;
    [SerializeField] JumpAttack jumpAtck;
    [SerializeField] LandAttack landAtck;
    
    
    BasicAttack basicAttack;

    NormalAttack attack;
    ShockDownAttack shockDownAttack;
    ShortAirSlash shortAirSlash;


    public void Awake()
    {
        fsm = new StateMachine<State>();
        transformPos = GetComponent<TransformPos>();
        keys = GetComponent<KeyManager>();
        basicAttack = new BasicAttack();
        SetStateData(walk);
        SetStateData(run);
        SetStateData(jumpUp);
        SetStateData(jumpDown);
        SetStateData(idle);
        SetStateData(land);
        SetStateData(hit);
        SetStateData(down);
        SetStateData(fall);
        SetStateData(sit);
        SetStateData(itemPick);

        SetStateData(jumpAtck);
        SetStateData(landAtck);

        SetStateData(interact);
        SetStateData(basicAttack);
        

        fsm.AddState(State.JumpUp, jumpUp);
        fsm.AddState(State.JumpDown, jumpDown);
        fsm.AddState(State.Interaction, interact);
        fsm.AddState(State.Walk, walk);
        fsm.AddState(State.Run, run);
        fsm.AddState(State.Idle, idle);
        fsm.AddState(State.Land, land);
        fsm.AddState(State.Hit, hit);
        fsm.AddState(State.Fall, fall);
        fsm.AddState(State.Down, down);
        fsm.AddState(State.ItemPick, itemPick);
        fsm.AddState(State.Sit, sit);
        fsm.AddState(State.JumpAtck, jumpAtck);
        fsm.AddState(State.LandAttack, landAtck);
        fsm.AddState(State.BasicAtck, basicAttack);

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
        AttackState attack = state as AttackState;
        if (attack != null)
            attack.SettingAttack();
    }

    void GetSkill()
    {
        attack = GetComponent<NormalAttack>();
        shockDownAttack = GetComponent<ShockDownAttack>();
        shortAirSlash = GetComponent<ShortAirSlash>();
    }

    private void Update()
    {
        fsm.Update();
        keys.ResetLayer();
    }

    private void FixedUpdate() => fsm.FixedUpdate();

    public void OnStartAlert()
    {
        if (Alertcoroutine != null)
            StopCoroutine(Alertcoroutine);
        Alertcoroutine = StartCoroutine(StartAlert());
        isAlert = true;
    }
    Coroutine Alertcoroutine = null;
    IEnumerator StartAlert()
    {
        yield return new WaitForSeconds(alertTime);
        isAlert = false;
    }
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
