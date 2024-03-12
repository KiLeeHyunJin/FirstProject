using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ShortAirSlashController : SkillStateController
{
    protected override void EnterState()
    {
        owner.SetState = PlayerState.AirSlash0;
    }
}
