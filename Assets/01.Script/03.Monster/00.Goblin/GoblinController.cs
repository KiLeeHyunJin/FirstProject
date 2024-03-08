using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoblinState
{
    Idle, Walk,
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

        fsm.AddState(GoblinState.Atck,atck);
        fsm.AddState(GoblinState.Down,down);
        fsm.AddState(GoblinState.Fall,fall);
        fsm.AddState(GoblinState.Hit, hit);
        fsm.AddState(GoblinState.Idle,idle);
        fsm.AddState(GoblinState.Sit, sit);
        fsm.AddState(GoblinState.Walk, walk);
    }
    protected override void Start()
    {
        base.Start();
        fsm.Start(GoblinState.Idle);
    }

    public override void ISetDamage(float damage)
    {
        SetState = GoblinState.Fall;
    }
    public override void ISetType()
    {
        base.ISetType();
    }
}
