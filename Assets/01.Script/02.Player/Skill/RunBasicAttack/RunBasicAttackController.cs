using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RunBasicAttackController : SkillStateController
{
    protected override void EnterState()
    {
        owner.SetState = PlayerState.RunAtck;
    }
}
