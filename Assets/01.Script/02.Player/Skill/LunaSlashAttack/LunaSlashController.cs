using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LunaSlashController : SkillStateController
{
    public BaseProjectile luna;
    public float ChargeTime { get; set; }
    protected override void EnterState()
    {
        if (luna == null)
            luna = GameObject.Instantiate(projectile);
        owner.activeType = ActiveType.Skill;
        owner.SetState = PlayerState.LunaSlashAttack0;
    }

}
