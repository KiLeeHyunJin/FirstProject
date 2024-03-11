using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum TauState
{
    Idle, Walk,
    Around, Chase,
    AtckReady, AtckReady1,
    AtckFinish,
    Atck1,Atck2, Atck3,
    Hit,
    Fall, Down,
}
public class TauController : MonsterController<TauState>
{

    [SerializeField] Tau_Atck1 atck1;
    [SerializeField] Tau_Atck2 atck2;
    [SerializeField] Tau_Atck3 atck3;
    [SerializeField] Tau_AtckFinish atckFinish;
    [SerializeField] Tau_AtckReady atckReady;
    [SerializeField] Tau_AtckReady1 atckReady1;
    [SerializeField] Tau_Down down;
    [SerializeField] Tau_Hit hit;
    [SerializeField] Tau_Fall fall;
    [SerializeField] Tau_Idle idle;
    [SerializeField] Tau_Walk walk;
    [SerializeField] Tau_Around around;
    [SerializeField] Tau_Chase chase;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        SetStateClass(atck1);
        SetStateClass(atck2);
        SetStateClass(atck3);
        SetStateClass(atckFinish);
        SetStateClass(atckReady);
        SetStateClass(atckReady1);
        SetStateClass(down);
        SetStateClass(hit);
        SetStateClass(fall);
        SetStateClass(idle);
        SetStateClass(walk);
        SetStateClass(around);
        SetStateClass(chase);

        fsm.AddState(TauState.Atck1, atck1);
        fsm.AddState(TauState.Atck2, atck2);
        fsm.AddState(TauState.Atck3, atck3);
        fsm.AddState(TauState.Down, down);
        fsm.AddState(TauState.Fall, fall);
        fsm.AddState(TauState.Hit, hit);
        fsm.AddState(TauState.Idle, idle);
        fsm.AddState(TauState.AtckFinish, atckFinish);
        fsm.AddState(TauState.AtckReady, atckReady);
        fsm.AddState(TauState.AtckReady1, atckReady1);
        fsm.AddState(TauState.Walk, walk);
        fsm.AddState(TauState.Around, around);
        fsm.AddState(TauState.Chase, chase);

        fsm.Start(TauState.Idle);
    }

    public override void ISetDamage(int damage, AttackEffectType effectType)
    {
        if (
            CurrentState == TauState.Fall ||
            CurrentState == TauState.Down)
        {
            if (effectType != AttackEffectType.Down)
                return;
        }
        switch (effectType)
        {
            case AttackEffectType.Little:
                SetState = TauState.Hit;
                break;
            case AttackEffectType.Stun:
                SetState = TauState.Hit;
                break;
            case AttackEffectType.Down:
                SetState = TauState.Fall;
                break;
            default:
                break;
        }
        MinusHp = damage;
    }
    public override void ISetType()
    {
        base.ISetType();
    }
    [SerializeField] Vector2 offset;
    [SerializeField] Vector3 size;


    bool isStart;
    private void OnDrawGizmos()
    {
        Vector2 gizmoSize = size;
        float checkLength = gizmoSize.x > gizmoSize.y ? gizmoSize.x : gizmoSize.y;
        int dir = transformPos.direction == TransformPos.Direction.Left ? -1 : 1;
        float temp = isStart ? 1 : 0.5f;
        //범위
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(
        new Vector2(transformPos.X, transformPos.Z) + new Vector2(offset.x * dir, 0),
            new Vector2(size.x, size.z) * temp * 2);
        //높이
        //Gizmos.color = UnityEngine.Color.yellow;
        float realYPos =
            (transformPos.Z + transformPos.Y) + (gizmoSize.y * temp * 0.5f);
        //Gizmos.DrawWireSphere(new Vector2(transformPos.X, realYPos) + new Vector2(offset.x * dir, offset.y), checkLength * temp);

        Gizmos.color = UnityEngine.Color.green;
        Gizmos.DrawWireCube(new Vector2(transformPos.X, realYPos) + new Vector2(offset.x * dir , offset.y),
        new Vector2(size.x * temp * 2, size.y * temp));

    }
}
