using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LunaSlashController : SkillStateController
{
    public ProjectileObj luna;
    public float ChargeTime { get; set; }
    protected override void EnterState()
    {
        if (luna == null)
            luna = GameObject.Instantiate(projectile);
        owner.SetState = PlayerState.LunaSlashAttack0;
    }

}
