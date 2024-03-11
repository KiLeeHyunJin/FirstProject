using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoblinState
{
    Idle, Walk,
    Chase, Around,
    Hit, Fall, Down,
    Sit,
    Atck
}
public class GoblinController : MonsterController<GoblinState>
{
    [SerializeField] Goblin_Atck atck;
    [SerializeField] Goblin_Down down;
    [SerializeField] Goblin_Fall fall;
    [SerializeField] Goblin_Hit hit;
    [SerializeField] Goblin_Idle idle;
    [SerializeField] Goblin_Sit sit;
    [SerializeField] Goblin_Walk walk;
    [SerializeField] Goblin_Around around;
    [SerializeField] Goblin_Chase chase;

    protected override void Awake()
    {
        base.Awake();
        SetStateClass(atck);
        SetStateClass(down);
        SetStateClass(fall);
        SetStateClass(hit);
        SetStateClass(idle);
        SetStateClass(sit);
        SetStateClass(walk);
        SetStateClass(around);
        SetStateClass(chase);

        fsm.AddState(GoblinState.Atck,atck);
        fsm.AddState(GoblinState.Down,down);
        fsm.AddState(GoblinState.Fall,fall);
        fsm.AddState(GoblinState.Hit, hit);
        fsm.AddState(GoblinState.Idle,idle);
        fsm.AddState(GoblinState.Sit, sit);
        fsm.AddState(GoblinState.Walk, walk);
        fsm.AddState(GoblinState.Around, around);
        fsm.AddState(GoblinState.Chase, chase);
    }
    protected override void Start()
    {
        base.Start();
        fsm.Start(GoblinState.Idle);
    }

    public override void ISetDamage(float damage, AttackEffectType effectType)
    {
        if (
            CurrentState == GoblinState.Fall ||
            CurrentState == GoblinState.Down)
        {
            if (effectType != AttackEffectType.Down)
                return;
        }

        switch (effectType)
        {
            case AttackEffectType.Little:
                SetState = GoblinState.Hit;
                break;
            case AttackEffectType.Stun:
                SetState = GoblinState.Hit;
                break;
            case AttackEffectType.Down:
                SetState = GoblinState.Fall;
                break;
            default:
                break;
        }
    }
    public override void ISetType()
    {
        base.ISetType();
    }
}
