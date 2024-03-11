using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Goblin_Walk : MonsterState<GoblinState>
{
    public enum WalkType{ Chase,Non    }
    WalkType type;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.WalkId);
        if (owner.sensor.target != null)
        {
            if(owner.CheckCoolTime(MonsterController<GoblinState>.AtckEnum.Atck1))
            {
                type = WalkType.Chase;
                return;
            }
        }
        type = WalkType.Non;
    }
    public override void Transition()
    {
        if (type == WalkType.Chase)
            owner.SetState = GoblinState.Chase;
        else
            owner.SetState = GoblinState.Around;
    }
}
