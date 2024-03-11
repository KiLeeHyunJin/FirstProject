using System;
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
    A,S,D,F,G,Q,W,E,R,T
}
public enum AttackType
{ 
    Stand, Down, Jump,
}
public enum AttackEffectType
{
    Little, Stun, Down
}

public class PlayerController : BaseController<PlayerState>
{
    [field: SerializeField] public int Mp { get; private set; }
    public int MinusMp { set { Mp -= value; } }
    public int AddHp { set { Hp += value; } }
    public int AddMp { set { Mp += value; } }

    public PlayerState WalkType { get; set; }

    [SerializeField] float alertTime;

    public AnimIdTable animId { get; private set; }
    [Header("¸µÅ©")]
    [SerializeField] AttackController atkController;

    public KeyManager keys { get; private set; }
    public bool isAlert { get; private set; }
    public Vector2 moveValue { get; private set; }

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
    AttackContainer attackContainer;
    Dictionary<KeyManager.Key, AttackState> SkillDic = new Dictionary<KeyManager.Key, AttackState>();

    protected override void Awake()
    {
        base.Awake();
        //SkillDic ;
        keys = GetComponent<KeyManager>();
        attackContainer = GetComponent<AttackContainer>();
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
    protected override void Start()
    {
        fsm.Start(PlayerState.Idle);
        attackContainer.AddSkill("shockDown", KeyManager.Key.A);
        attackContainer.AddSkill("airSlash", KeyManager.Key.S);
    }

    void SetStateData(PlayerBaseState<PlayerState> state)
    {
        state.Setting(anim, transformPos, renderer );
        state.SetStateMachine(fsm);
        state.SetController(this);
        state.SettingAttackData(atkController);
        AttackState attack = state as AttackState;
        if (attack != null)
            attack.SettingAttack();
    }

    protected override void Update()
    {
        base.Update();
        AroundCheckQuickKey();
        keys.ResetLayer();
    }

    public void AroundCheckQuickKey()
    {
        if (CurrentState == PlayerState.Hit || 
            CurrentState == PlayerState.Fall || 
            CurrentState == PlayerState.Fall)
            return;

        foreach (KeyManager.Key key in keys.QuickKey)
        {
            if(keys.ContainLayer(key))
            {
                AttackState atckState = TryGetKey(key);
                if (atckState == null)
                    continue;

                if (CurrentState == PlayerState.JumpUp ||
                    CurrentState == PlayerState.JumpDown )
                    if(atckState.SpaceType != AttackType.Jump)
                    continue;
                PlayerState state = EnumUtil<PlayerState>.Parse(key.ToString());
                if (CurrentState == state)
                    atckState.Enter();
                else
                    SetState = state;
                return;
            }
        }
    }

    public void SetKeyDic(AttackState atckState, KeyManager.Key key)
    {
        if(atckState == null)
            return;
        PlayerState state = EnumUtil<PlayerState>.Parse(key.ToString());
        SetStateData(atckState);
        SkillDic.Add(key, atckState);
        fsm.AddState( state, atckState);
    }
    public AttackState TryGetKey(KeyManager.Key checkKey)
    {
        SkillDic.TryGetValue(checkKey, out var element);
        return element;
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

    public override void ISetDamage(int damage, AttackEffectType effectType)
    {
        if (CurrentState == PlayerState.Fall ||
            CurrentState == PlayerState.Down)
        {
            if (effectType != AttackEffectType.Down)
                return;
        }
        switch (effectType)
        {
            case AttackEffectType.Little:
                SetState = PlayerState.Hit;
                break;
            case AttackEffectType.Stun:
                SetState = PlayerState.Hit;
                break;
            case AttackEffectType.Down:
                SetState = PlayerState.Fall;
                break;
            default:
                break;
        }
        MinusHp = damage;
        
    }
}
public static class EnumUtil<T>
{
    public static T Parse(string s)
    {
        return (T)Enum.Parse(typeof(T), s);
    }
}