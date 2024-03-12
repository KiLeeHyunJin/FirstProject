using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LunaSlashController : SkillStateController
{
    public float ChargeTime { get; set; }
    protected override void EnterState()
    {
        owner.SetState = PlayerState.LunaSlashAttack0;
    }

}
