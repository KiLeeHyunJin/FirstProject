using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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

    
    X, BasicAtck, ItemPick,
    
    Action,
    A,S,D,F,G,Q,W,E,R,T,
    AirSlashControll,
    AirSlash0,AirSlash1,AirSlash2,
    LandAtckControll,
    LandAtck0, LandAtck1, LandAtck2,
    JumpAtckControll,
    JumpAtck,
    RunAtckControll,
    RunAtck,
    ShockWaveAttackControll,
    ShockWaveAttack,
    HeatWaveAttackControll,
    HeatWaveAttack0, HeatWaveAttack1,
    LunaSlashAttack0, LunaSlashAttack1,
    Non,

}
public enum AttackType
{ 
    Stand, Down, Jump,
}
public enum AttackEffectType
{
    Little, Stun, Down
}
public enum ActiveType
{
    Normal, Skill,
}

public class PlayerController : BaseController<PlayerState>
{
    [field: SerializeField] public int Mp { get; private set; }
    public int MinusMp { set { Mp -= value; } }
    public int AddHp { set { Hp += value; } }
    public int AddMp { set { Mp += value; } }

    public PlayerState WalkType { get; set; }
    public ActiveType activeType { get; set; }

    [SerializeField] float alertTime;

    public AnimIdTable animId { get; private set; }
    [Header("¸µÅ©")]
    [SerializeField] AttackController atkController;

    public KeyManager keys { get; private set; }
    public bool isAlert { get; private set; }
    public Vector2 moveValue { get; private set; }

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

    AttackContainer attackContainer;
    Dictionary<KeyManager.QuickKey, SkillStateController> SkillDic = new Dictionary<KeyManager.QuickKey, SkillStateController>();

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

        SetStateData(interact);
        SetStateData(basicAttack);
        

        fsm.AddState(PlayerState.JumpUp, jumpUp);
        fsm.AddState(PlayerState.JumpDown, jumpDown);
        fsm.AddState(PlayerState.X, interact);
        fsm.AddState(PlayerState.Walk, walk);
        fsm.AddState(PlayerState.Run, run);
        fsm.AddState(PlayerState.Idle, idle);
        fsm.AddState(PlayerState.Land, land);
        fsm.AddState(PlayerState.Hit, hit);
        fsm.AddState(PlayerState.Fall, fall);
        fsm.AddState(PlayerState.Down, down);
        fsm.AddState(PlayerState.ItemPick, itemPick);
        fsm.AddState(PlayerState.Sit, sit);
        fsm.AddState(PlayerState.BasicAtck, basicAttack);




    }
    protected override void Start()
    {
        fsm.Start(PlayerState.Idle);

        GetSkill("LandBasic", PlayerState.LandAtckControll,
        new PlayerState[]
        { PlayerState.LandAtck0, PlayerState.LandAtck1, PlayerState.LandAtck2 });

        GetSkill("RunBasic", PlayerState.RunAtckControll,
            new PlayerState[]
        { PlayerState.RunAtck});

        GetSkill("JumpBasic", PlayerState.JumpAtckControll,
            new PlayerState[]
        { PlayerState.JumpAtck });

        GetSkill("ShortAir", KeyManager.QuickKey.A,
            new PlayerState[]
        { PlayerState.AirSlash0, PlayerState.AirSlash1,PlayerState.AirSlash2 });

        GetSkill("ShockWave", KeyManager.QuickKey.S,
        new PlayerState[]
        { PlayerState.ShockWaveAttack });

        GetSkill("HeatWave", KeyManager.QuickKey.D,
        new PlayerState[]
        { PlayerState.HeatWaveAttack0, PlayerState.HeatWaveAttack1 });

        GetSkill("LunaSlash", KeyManager.QuickKey.F,
        new PlayerState[]
        { PlayerState.LunaSlashAttack0, PlayerState.LunaSlashAttack1 });
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

    void GetSkill(string str, KeyManager.QuickKey setKey, params PlayerState[] enums)
    {
        SkillContain contain = attackContainer.GetSkillData(str);
        SetStateData(contain.controller);
        for (int i = 0; i < contain.attackStates.Count; i++)
            SetStateData(contain.attackStates[i]);
        SetKeyDic(contain.controller, setKey);
        contain.controller.currentSkillKey = setKey;

        for (int i = 0; i < enums.Length; i++)
            fsm.AddState(enums[i], contain.attackStates[i]);
    }

    void GetSkill(string str, PlayerState state  , params PlayerState[] enums)
    {
        SkillContain contain = attackContainer.GetSkillData(str);
        SetStateData(contain.controller);
        for (int i = 0; i < contain.attackStates.Count; i++)
            SetStateData(contain.attackStates[i]);
        fsm.AddState(state, contain.controller);
        for (int i = 0; i < enums.Length; i++)
            fsm.AddState(enums[i], contain.attackStates[i]);
    }
    protected override void Update()
    {
        base.Update();
        AroundCheckQuickKey();
        keys.ResetLayer();
        //keys.Re();
    }

    public void AroundCheckQuickKey()
    {
        if (CurrentState == PlayerState.Hit || 
            CurrentState == PlayerState.Fall || 
            CurrentState == PlayerState.Fall)
            return;
        if (activeType == ActiveType.Skill)
            return;
        foreach (KeyManager.QuickKey key in keys.QuickKeys)
        {
            if(keys.ContainLayer(key))
            {
                SkillStateController atckState;
                atckState = TryGetKey(key);
                if (atckState == null)
                    continue;

                if (CurrentState == PlayerState.JumpUp ||
                    CurrentState == PlayerState.JumpDown )
                    if(atckState.AttackType != AttackType.Jump)
                    continue;

                PlayerState state = EnumUtil<PlayerState>.Parse(key.ToString());
                if (atckState.On)
                    atckState.Click = true;
                else
                    SetState = state;
                return;
            }
        }
    }
    public void SetKeyDic(SkillStateController atckState, KeyManager.DefaultKey key)
    {
        if (atckState == null)
            return;
        PlayerState state = EnumUtil<PlayerState>.Parse(key.ToString());
        SetStateData(atckState);
        fsm.AddState(state, atckState);
    }
    public void SetKeyDic(SkillStateController atckState, KeyManager.QuickKey key)
    {
        if(atckState == null)
            return;
        PlayerState state = EnumUtil<PlayerState>.Parse(key.ToString());
        SetStateData(atckState);
        SkillDic.Add(key, atckState);
        fsm.AddState( state, atckState);
    }
    public SkillStateController TryGetKey(KeyManager.QuickKey checkKey)
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