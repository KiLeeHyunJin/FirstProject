using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JumpBasicAttackController : SkillStateController
{
    protected override void EnterState()
    {
        owner.SetState = PlayerState.JumpAtck;
    }
}
