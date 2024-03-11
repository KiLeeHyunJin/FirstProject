using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Tau_Walk : MonsterState<TauState>
{
    enum WalkType { Chase, Non }
    WalkType type;
    Transform target;
    public override void Enter()
    {
        anim.Play(AnimIdTable.GetInstance.WalkId);
        target = owner.sensor.target;

        if (target != null)
        {
            foreach (MonsterController<TauState>.AtckEnum item in Enum.GetValues(typeof(MonsterController<TauState>.AtckEnum)))
            {
                if (owner.CheckCoolTime(item))
                {
                    type = WalkType.Chase;
                    return;
                }
            }
        }
        type = WalkType.Non;
    }
    public override void Transition()
    {
        if (type == WalkType.Chase)
            owner.SetState = TauState.Chase;
        else
            owner.SetState = TauState.Around;
    }
}
