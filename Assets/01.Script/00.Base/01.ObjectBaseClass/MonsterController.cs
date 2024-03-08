using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController<T> : BaseController<T> where T : Enum
{
    public TargetSensor sensor;
    protected override void Start()
    {
         sensor = sensor == null ? GetComponentInChildren<TargetSensor>() : sensor;
    }

    protected void SetStateClass(BaseState<T> state)
    {
        state.SetStateMachine(fsm);
        state.Setting(anim, transformPos, renderer, this);
    }
}
