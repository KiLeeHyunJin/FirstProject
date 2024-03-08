using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static KeyManager;
using static UnityEditor.PlayerSettings;
public enum PlayerState
{
    Idle,
    Walk, Run,
    JumpUp, JumpDown, Land,
    Hit, Stune,
    Fall, Down, Sit,

    JumpAtck, RunAtck, LandAtck,
    Interaction, BasicAtck, ItemPick,

    Action,
}
public class PlayerController : BaseController<PlayerState>
{
    [field: SerializeField] public PlayerState WalkType { get; set; }

    [SerializeField] float alertTime;

    public AnimIdTable animId { get; private set; }
    [Header("¸µÅ©")]
    [SerializeField] AttackController atkController;

    [field: SerializeField] public KeyManager keys { get; private set; }
    public bool isAlert { get; private set; }
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
    [SerializeField] BasicAttack basicAttack;

    [SerializeField] JumpAttack jumpAtck;
    [SerializeField] LandAttack landAtck;
    [SerializeField] RunAttack runAtck;



    NormalAttack attack;
    ShockDownAttack shockDownAttack;
    ShortAirSlash shortAirSlash;


    protected override void Awake()
    {
        base.Awake();
        keys = GetComponent<KeyManager>();
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
        SetStateData(runAtck);

        SetStateData(jumpAtck);
        SetStateData(landAtck);

        SetStateData(interact);
        SetStateData(basicAttack);
        

        fsm.AddState(PlayerState.JumpUp, jumpUp);
        fsm.AddState(PlayerState.JumpDown, jumpDown);
        fsm.AddState(PlayerState.Interaction, interact);
        fsm.AddState(PlayerState.Walk, walk);
        fsm.AddState(PlayerState.Run, run);
        fsm.AddState(PlayerState.Idle, idle);
        fsm.AddState(PlayerState.Land, land);
        fsm.AddState(PlayerState.Hit, hit);
        fsm.AddState(PlayerState.Fall, fall);
        fsm.AddState(PlayerState.Down, down);
        fsm.AddState(PlayerState.ItemPick, itemPick);
        fsm.AddState(PlayerState.Sit, sit);
        fsm.AddState(PlayerState.JumpAtck, jumpAtck);
        fsm.AddState(PlayerState.LandAtck, landAtck);
        fsm.AddState(PlayerState.BasicAtck, basicAttack);
        fsm.AddState(PlayerState.RunAtck, runAtck);

    }
    public void Start()
    {
        GetSkill();
        //anim = GetComponentInChildren<Animator>();
        fsm.Start(PlayerState.Idle);
    }

    void SetStateData(PlayerBaseState<PlayerState> state)
    {
        state.Setting(anim, transformPos, renderer, this );
        state.SetStateMachine(fsm);
        state.SettingAttackData(atkController);
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

    protected override void Update()
    {
        base.Update();
        keys.ResetLayer();
    }


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

    public void CallDown()
    {
        SetState = PlayerState.Fall;
    }
}
